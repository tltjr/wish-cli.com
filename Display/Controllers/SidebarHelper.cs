using System.Linq;
using Display.Data;
using Display.Models;

namespace Display.Controllers
{
    public class SidebarHelper
    {
        private readonly PostRepository _postRepository = new PostRepository();

        public SidebarModel GetSidebarModel()
        {
            var posts = _postRepository.FindAll().ToList();
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var latest = posts.Take(5);
            var categories = _postRepository.FindTags();
            return new SidebarModel()
                       {
                           Categories = categories,
                           Latest = latest
                       };
        }
    }
}