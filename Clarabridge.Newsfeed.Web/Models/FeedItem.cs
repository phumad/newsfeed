using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clarabridge.Newsfeed.Web.Models
{
    public class FeedItem
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Text { get; set; }
        public bool InFullText { get; set; }
    }
}