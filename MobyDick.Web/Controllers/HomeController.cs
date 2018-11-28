using MobyDick.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MobyDick.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string _savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/mobydick.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<BookViewModel>));
            List<BookViewModel> wordList = new List<BookViewModel>();
            var words = (from c in XElement.Load(_savePath).Elements("word")
                       select c).Take(10);

            foreach (var item in words)
            {
                wordList.Add(new BookViewModel { word = item.Attribute("text").Value, count = item.Attribute("count").Value });
            }
            
            return View(wordList);
        }
    }
}