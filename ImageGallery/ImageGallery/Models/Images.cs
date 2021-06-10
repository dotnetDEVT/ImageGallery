using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class Images
    {
        public int ImagesID { get; set; }
        public string ImageName { get; set; }
        public string CreatedDate { get; set; }
    }
    public class Date_Request
    {
        public string Date { get; set; }
        public string Src { get; set; }
    }


    public class Rootobject
    {
        public Photo[] photos { get; set; }
    }

    public class Photo
    {
        public int id { get; set; }
        public int sol { get; set; }
        public Camera camera { get; set; }
        public string img_src { get; set; }
        public string earth_date { get; set; }
        public Rover rover { get; set; }
    }

    public class Camera
    {
        public int id { get; set; }
        public string name { get; set; }
        public int rover_id { get; set; }
        public string full_name { get; set; }
    }

    public class Rover
    {
        public int id { get; set; }
        public string name { get; set; }
        public string landing_date { get; set; }
        public string launch_date { get; set; }
        public string status { get; set; }
    }

}
