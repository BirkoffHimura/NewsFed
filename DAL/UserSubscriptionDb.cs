using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserSubscriptionDb
    {
        private NFedContext db;
        public UserSubscriptionDb()
        {
            db = new NFedContext();
        }
        public UserSubscriptionDb(bool local)
        {
            if (local)
            {
                db = new NFedContext(true);
            }
            else
            {
                db = new NFedContext();
            }
        }
        public IEnumerable<UserSubscription> GetAll()
        {
            return db.UserSubscriptions.ToList();
        }
        public UserSubscription GetByID(long Id)
        {
            return db.UserSubscriptions.Find(Id);
        }

        public UserSubscription GetByFeedAndSubID(long FeedId, long SubId)
        {
            var ret = db.UserSubscriptions
                .Include(u => u.Feed_ID)
                .Where(x => x.User_Feed_ID == FeedId && x.User_Sub_ID == SubId)
                .FirstOrDefault();
            
            return ret;
        }

        public IEnumerable<UserSubscription> GetSubsByFeedID(long Id)
        {
            return db.UserSubscriptions.Where(x => x.User_Feed_ID == Id);
        }

        public IEnumerable<UserSubscription> GetFeedsBySubID(long Id)
        {
            return db.UserSubscriptions.Where(x => x.User_Sub_ID == Id);
        }

        public IEnumerable<UserSubscription> GetSubsByFeedUserName(string userName)
        {
            User user = db.Users.Where(x => x.UserName == userName).FirstOrDefault();
            long Id = user != null ? user.ID : -1;

            return db.UserSubscriptions.Where(x => x.User_Feed_ID == Id).ToList();            
        }

        public IEnumerable<UserSubscription> GetFeedsBySubUserName(string userName)
        {
            User user = db.Users
                .Where(x => x.UserName == userName)
                .FirstOrDefault();
            long Id = user != null ? user.ID : -1;

            var ret = db.UserSubscriptions
                .Include(uf => uf.Feed_ID)
                .Where(x => x.User_Sub_ID == Id)
                .ToList();
            
            return ret;
        }

        public int Insert(UserSubscription userSubscription)
        {
            db.UserSubscriptions.Add(userSubscription);
            return Save();
        }

        public int Delete(long Id)
        {
            UserSubscription userSubscription = GetByID(Id);
            db.UserSubscriptions.Remove(userSubscription);
            return Save();
        }

        

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
