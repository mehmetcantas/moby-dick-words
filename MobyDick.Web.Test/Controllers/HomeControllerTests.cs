using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobyDick.Web.Controllers;
using MobyDick.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MobyDick.Web.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod]
        public void CheckReturnedModel()
        {
            // Arrange  
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            var bookModel = (List<BookViewModel>)result.ViewData.Model;

            Assert.AreEqual(10, bookModel.Count);
        }
    }
}