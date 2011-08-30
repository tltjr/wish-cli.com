using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using Display.Models;

namespace Display.Feed
{
    public class RssHelper
    {
        public IEnumerable<SyndicationItem> CreateSyndicationItems(IEnumerable<Post> twenty)
        {
            return
                twenty.Select(
                    post =>
                    new SyndicationItem(post.Title, post.Body, new Uri(@"http://wish-cli.com/Blog/" + post.Slug)));
        }
    }
}