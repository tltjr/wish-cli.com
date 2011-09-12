using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Display.Models
{
    public class SinglePostModel
    {
        public Post Post { get; set; }
        public SidebarModel Sidebar { get; set; }
    }
}