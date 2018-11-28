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
           XElement xElement = null;

            if (HttpContext.Cache[_savePath] == null)
            {
                string cacheFilePath = _savePath;
                xElement = XElement.Load(cacheFilePath);
                HttpContext.Cache.Insert(_savePath, xElement, new CacheDependency(cacheFilePath));
            }
            else
            {
                xElement = (XElement)HttpContext.Cache[_savePath];
            }
            var words = (from c in xElement.Elements("word")
                         select c).Take(10);


            foreach (var item in words)
            {
                wordList.Add(new BookViewModel { word = item.Attribute("text").Value, count = item.Attribute("count").Value });
            }
            
            return View(wordList);
        }
    }
}
