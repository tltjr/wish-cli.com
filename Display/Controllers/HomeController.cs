using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Display.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Page()
        {
            throw new NotImplementedException();
        }

        public ActionResult PageFull()
        {
            throw new NotImplementedException();
        }
    }
}
