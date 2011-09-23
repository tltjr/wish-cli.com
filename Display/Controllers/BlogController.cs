using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
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
        private readonly SidebarHelper _sidebarHelper = new SidebarHelper();

        public ActionResult Index()
        {
            List<Post> posts;
            try
            {
                posts = _postRepository.FindAll().ToList();
            }
            catch(Exception e){
                return RedirectToAction("Error", "Preview", new { errorType = "Database" });
            }
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var sidebar = _sidebarHelper.GetSidebarModel();
            var blogModel = new BlogModel()
                                {
                                    Posts = posts.Take(10),
                                    Sidebar = sidebar
                                };
            return View(blogModel);
        }

        public ActionResult Registered()
        {
            List<Post> posts;
            try
            {
                posts = _postRepository.FindAll().ToList();
            }
            catch(Exception e){
                return RedirectToAction("Error", "Preview", new { errorType = "Database" });
            }
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var sidebar = _sidebarHelper.GetSidebarModel();
            var blogModel = new BlogModel()
                                {
                                    Posts = posts.Take(10),
                                    Sidebar = sidebar
                                };
            return View(blogModel);
        }

        [HttpPost]
        public bool Registered(string email, string tweeter)
        {
            if (!String.IsNullOrEmpty(email))
            {
                var repository = new EmailRepository();
                var model = new EmailModel() { Email = email, Tweeter = tweeter };
                try
                {
                    repository.Store(model);
                    return true;
                }
                catch (Exception e) {
                    //potentially stash emails in memory until db is back up. Don't want to lose them!
                }
            }
            return false;
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
                var trimmed = split.Select(o => o.Trim());
                post.Tags = UppercaseFirst(trimmed).ToList();
            }
            _postRepository.Store(post);
            return RedirectToAction("Single", new { slug = post.Slug });
        }

        private static IEnumerable<string> UppercaseFirst(IEnumerable<string> strings)
        {
            foreach (var str in strings)
            {
            	if (string.IsNullOrEmpty(str))
            	{
            	    yield return string.Empty;
            	}
            	yield return char.ToUpper(str[0]) + str.Substring(1).ToLower();
            }
        }

        public ActionResult Single(string slug)
        {
            Post post;
            try
            {
                post = _postRepository.FindOneByKey("Slug", slug);
            }
            catch(Exception e){
                return RedirectToAction("Error", "Preview", new { errorType = "Database" });
            }
            var sidebar = _sidebarHelper.GetSidebarModel();
            var singlePostModel = new SinglePostModel()
                                      {
                                          Post = post,
                                          Sidebar = sidebar
                                      };
            return View(singlePostModel);
        }


        public PartialViewResult Entry(string slug)
        {
            var post = _postRepository.FindOneByKey("Slug", slug);
            return PartialView("_Entry", post);
        }

        public ActionResult Tag(string tag)
        {
            try
            {
                var tagged = new TaggedModel
                                 {
                                     Posts = _postRepository.FindAllByKey("Tags", tag),
                                     Tag = tag,
                                     Sidebar = _sidebarHelper.GetSidebarModel()
                                 };
                return View(tagged);
            }
            catch(Exception e) {
                return RedirectToAction("Error", "Preview", new { errorType = "Database" });
            }
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
            try
            {
                var posts = _postRepository.FindAll().ToList();
                posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
                var twenty = posts.Take(20);
                var feed = new SyndicationFeed("Wish Blog",
                    "News about the ultimate command line interface for Windows, Wish!",
                    new Uri(@"http://wish-cli.com/Feed"),
                    _rssHelper.CreateSyndicationItems(twenty));
                return new RssActionResult { Feed = feed };
            }
            catch (Exception e) {
                return null;
            }
        }
    }
}
