using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Display.Models
{
    public class SidebarModel
    {
        public IEnumerable<Post> Latest { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}