using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Display.Data;

namespace Display.Controllers
{
    public class BlogController : Controller
    {
        private readonly PostRepository _postRepository = new PostRepository();

        public ActionResult Index()
        {
            var posts = _postRepository.FindAll().ToList();
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var ten = posts.Take(10);
            return View(ten);
        }

        public ActionResult Single()
        {
            return View();
        }

        public ActionResult Archive()
        {
            return View();
        }
    }
}
