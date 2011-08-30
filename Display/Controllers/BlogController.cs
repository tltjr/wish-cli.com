using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using Display.Data;
using Display.Feed;
using Display.Models;
using MongoDB.Bson;

namespace Display.Controllers
{
    public class BlogController : Controller
    {
        private readonly PostRepository _postRepository = new PostRepository();
        private readonly RssHelper _rssHelper = new RssHelper();

        public ActionResult Index()
        {
            var posts = _postRepository.FindAll().ToList();
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var ten = posts.Take(10);
            return View(ten);
        }

        public ActionResult Registered()
        {
            ViewBag.Title = ConfigurationManager.AppSettings["title"];
            var posts = _postRepository.FindAll().ToList();
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            return View(posts.Take(10));
        }

        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(Post post)
        {
            post.CreatedAt = DateTime.Now;
            if(post.TagsRaw != null)
            {
                var split = post.TagsRaw.Split(',');
                post.Tags = split.Select(o => o.Trim()).ToList();
            }
            _postRepository.Store(post);
            return RedirectToAction("Post", new { slug = post.Slug });
        }

        public ActionResult Post(string slug)
        {
            ViewBag.Title = ConfigurationManager.AppSettings["title"];
            var post = _postRepository.FindOneByKey("Slug", slug);
            return View(post);
        }

        public ActionResult Tag(string tag)
        {
            ViewBag.Title = ConfigurationManager.AppSettings["title"];
            var tagged = new Tagged {Posts = _postRepository.FindAllByKey("Tags", tag), Tag = tag};
            return View(tagged);
        }

        public ActionResult Edit(string objectId)
        {
            var post = _postRepository.FindOneById(objectId);
            EditId.Id = new ObjectId(objectId);
            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            post.CreatedAt = DateTime.Now;
            if (post.TagsRaw != null)
            {
                var split = post.TagsRaw.Split(',');
                post.Tags = split.Select(o => o.Trim()).ToList();
            }
            _postRepository.Update(post);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            _postRepository.DeleteById(new ObjectId(id));
            return RedirectToAction("Index");
        }

        public ActionResult Archive()
        {
            var posts = _postRepository.FindAll().ToList();
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            return View(posts);
        }

        public ActionResult Rss()
        {
            var posts = _postRepository.FindAll().ToList();
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var twenty = posts.Take(20);
            var title = ConfigurationManager.AppSettings["title"];
            var description = ConfigurationManager.AppSettings["description"];
            var uri = ConfigurationManager.AppSettings["feeduri"];
            var feed = new SyndicationFeed(title, description, new Uri(uri),
                _rssHelper.CreateSyndicationItems(twenty));
            return new RssActionResult { Feed = feed };
        }
    }
}
