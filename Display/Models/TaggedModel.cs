using System.Collections.Generic;

namespace Display.Models
{
    public class TaggedModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public SidebarModel Sidebar { get; set; }
        public string Tag { get; set; }
    }
}