using System.Web.Mvc;

namespace Display.Controllers
{
    public class PreviewController : Controller
    {
        private readonly SidebarHelper _sidebarHelper = new SidebarHelper();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Single()
        {
            return View(_sidebarHelper.GetSidebarModel());
        }
    }
}