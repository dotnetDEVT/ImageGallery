using ImageGallery.Controllers;
using ImageGallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;

namespace ImageGalleyUnitTest
{
    [TestClass]
    public class ImageGallery
    {
        [TestMethod]
        public void GetDates()
        {
            //Arrange
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.WebRootPath)
                .Returns("../../../../ImageGallery/wwwroot");
            //Act
            ActionsController objC = new ActionsController(mockEnvironment.Object);
            int datecount = objC.GetAllDates().Count;

            //Assert
            Assert.IsTrue(datecount > 0, "There are no dates available");

        }

        [TestMethod]
        public void GetImageByDate()
        {
            // Arrange
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            ActionsController controller = new ActionsController(mockEnvironment.Object);
            // Act
            Date_Request objDate = new Date_Request();
            objDate.Date = "2017-11-27";
            var imagelist = controller.GetImages(objDate);

            //Assert
            Assert.IsNotNull(imagelist,"There are no images available");
        }
    }
}
