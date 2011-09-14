using System.Web.Mvc;

namespace Display.Controllers
{
    public class PreviewController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Screenshots()
        {
            return View();
        }

        public ActionResult Videos()
        {
            return View();
        }
    }
}