using ImageGallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ImageGallery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ActionsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static List<Images> images = new List<Images>();
        public ActionsController(IWebHostEnvironment webHostEnvironment)
        {
            images = new List<Images>();
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("GetAllDates")]
        public List<string> GetAllDates()
        {
            List<String> Date = new List<string>();
            try
            {
                FileStream fileStream = new FileStream(_webHostEnvironment.WebRootPath + "/Dates.txt", FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        Date.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Date;
        }
        // filter data based on selection from front end
        [HttpPost("GetImages")]
        public Rootobject GetImages([FromBody] Date_Request Date)
        {
            Rootobject responsez = null;
            try
            {
                var requestUrl = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=" + Date.Date + "&api_key=DEMO_KEY";
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.CreateDefault(new Uri(requestUrl));
                WebResponse res = (WebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream());
                string resultado = sr.ReadToEnd();
                res.Close();
                responsez = JsonConvert.DeserializeObject<Rootobject>(resultado);
            }
            catch (Exception ex)
            {
            }
            return responsez;
        }
        [HttpPost("Download")]
        public bool Download([FromBody] Date_Request Date)
        {
            //try
            //{
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(Date.Src), _webHostEnvironment.WebRootPath + "/images/" + DateTime.Now.Ticks + ".png");
                    return true;
                }
            //}
            //catch (Exception ex)
            //{

            //    return false;
            //}


        }
    }
}
