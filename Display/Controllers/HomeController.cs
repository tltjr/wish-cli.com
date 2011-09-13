using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Display.Controllers
{
    public class HomeController : Controller
    {
        private readonly SidebarHelper _sidebarHelper = new SidebarHelper();

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
            return View();
        }

        public ActionResult PageFull()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View(_sidebarHelper.GetSidebarModel());
        }
    }
}
