using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class NFedContext : DbContext
    {
        public NFedContext(bool test)
        {
            if (test)
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            }
            this.Configuration.ProxyCreationEnabled = false;
        }
        public NFedContext()
        {
            if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data")))
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));
            }
            else
            {               
                AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            }
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<NewsFeedItem> NewsFeedItems { get; set; }
        public DbSet<NewsFeedItemComment> NewsFeedItemComments { get; set; }
    }
}
