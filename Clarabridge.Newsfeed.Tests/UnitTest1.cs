using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Clarabridge.Newsfeed.Data;
using StructureMap;
using System.Collections.Generic;
using System.Data.Entity;

namespace Clarabridge.Newsfeed.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddFeed_adds()
        {
            var contextFactory = new Mock<IContextFactory>();
            var context = new Mock<FeedContext>();
            var dbset = new Mock<DbSet<Feed>>();
            var feed = new Feed();
            context.Setup(f => f.Feeds).Returns(dbset.Object);
            context.Setup(f => f.Feeds.Add(It.IsAny<Feed>())).Returns(feed);
            contextFactory.Setup(x => x.GetContext()).Returns(context.Object);

            var container = new Container(x =>
            {
                x.For<IContextFactory>().Use(contextFactory.Object);
                x.For<IFeedRepository>().Use<FeedRepository>();
            });

            var repo = container.GetInstance<IFeedRepository>();
            repo.AddFeed(new Feed());

            contextFactory.Verify(f => f.GetContext(), Times.Once);
            context.Verify(f => f.Feeds.Add(It.IsAny<Feed>()), Times.Once);
            context.Verify(c => c.SaveChanges(), Times.Once);


        }
    }
}
