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

        public FeedsController(IFeedRepository feedRepository)
        {
            this.feedRepository = feedRepository;
        }

        [HttpGet]
        [Route("api/feeds")]
        public IEnumerable<FeedItem> Get(int page, int pageSize, int bodySize)
        {
            page = page - 1;

            var response = feedRepository.GetFeeds(pageSize, page).Select(item => new FeedItem()
            {
                Id = item.Id,
                DateCreated = item.DateCreated,
                Text = item.Body.Substring(0, Math.Min(item.Body.Length, bodySize)),
                HasPartialText = item.Body.Length > bodySize
            });

            return response;           
        }

        [HttpGet]
        [Route("api/feeds/{id}", Name = "GetFeedById")]
        public HttpResponseMessage Get(int id)
        {
            var f = feedRepository.GetFeed(id);
            if (f == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Feed with id {0} not found", id));
            return Request.CreateResponse(f.Body);
        }

        [HttpPost]
        [Route("api/feeds")]
        public HttpResponseMessage Post([FromBody]string feed)
        {
            if (string.IsNullOrWhiteSpace(feed)) return Request.CreateResponse(HttpStatusCode.BadRequest);

            var f = feedRepository.AddFeed(new Feed() { Body = feed });
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Url.Link("GetFeedById", new { id = f.Id }));
            return response;
        }
    }
}
