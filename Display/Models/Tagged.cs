using System.Collections.Generic;

namespace Display.Models
{
    public class Tagged
    {
        public IEnumerable<Post> Posts { get; set; }
        public string Tag { get; set; }
    }
}