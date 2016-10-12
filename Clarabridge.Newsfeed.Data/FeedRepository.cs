using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarabridge.Newsfeed.Data
{
    public interface IFeedRepository
    {
        IEnumerable<Feed> GetFeeds(int pageSize, int page);
        Feed AddFeed(Feed feed);
        Feed GetFeed(int Id);
    }

    public class FeedRepository : IFeedRepository
    {
        public IContextFactory contextFactory;

        public FeedRepository(IContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public Feed AddFeed(Feed feed)
        {
            using (var context = contextFactory.GetContext())
            {
                feed.DateCreated = DateTime.Now;
                feed = context.Feeds.Add(feed);
                context.SaveChanges();
                return feed;
            }
        }

        public IEnumerable<Feed> GetFeeds(int pageSize, int page)
        {
            using (var context = contextFactory.GetContext())
            {
                return context.Feeds.OrderByDescending(f => f.DateCreated).Skip(pageSize * page).Take(pageSize).ToList();
            }
        }

        public Feed GetFeed(int Id)
        {
            using (var context = contextFactory.GetContext())
            {
                return context.Feeds.FirstOrDefault(feed => feed.Id == Id);
            }
        }
    }
}
