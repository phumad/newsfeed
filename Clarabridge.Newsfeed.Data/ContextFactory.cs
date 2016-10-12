using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarabridge.Newsfeed.Data
{
    public interface IContextFactory
    {
        FeedContext GetContext();
    }
    public class ContextFactory : IContextFactory
    {
        public FeedContext GetContext()
        {
            return new FeedContext();
        }
    }
}
