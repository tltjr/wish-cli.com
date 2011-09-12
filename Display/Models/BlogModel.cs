using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Display.Models
{
    public class BlogModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public SidebarModel Sidebar { get; set; }
    }
}