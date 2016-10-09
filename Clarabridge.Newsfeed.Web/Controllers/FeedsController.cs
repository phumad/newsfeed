using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Clarabridge.Newsfeed.Data;
using Clarabridge.Newsfeed.Web.Models;

namespace Clarabridge.Newsfeed.Web.Controllers
{
    public class FeedsController : ApiController
    {
        public IFeedRepository feedRepository;

        public FeedsController()
        {
            this.feedRepository = new FeedRepository();
        }

        [HttpGet]
        [Route("api/feeds")]
        public IEnumerable<FeedItem> Get(int page, int pageSize, int bodySize)
        {
            page = page - 1;

            return feedRepository.GetFeeds(pageSize, page).Select(item => new FeedItem()
            {
                Id = item.Id,
                DateCreated = item.DateCreated,
                Text = item.Body.Substring(0, Math.Min(item.Body.Length, bodySize)),
                InFullText = item.Body.Length > bodySize
            });
        }

        [HttpGet]
        [Route("api/feeds/{id}")]
        public Feed Get(int id)
        {
            return feedRepository.GetFeed(id);
        }

        [HttpPost]
        [Route("api/feeds")]
        public HttpResponseMessage Post([FromBody]string feed)
        {
            if (string.IsNullOrWhiteSpace(feed)) return Request.CreateResponse(HttpStatusCode.BadGateway);

            feedRepository.AddFeed(new Feed() { Body = feed });
            return this.Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
