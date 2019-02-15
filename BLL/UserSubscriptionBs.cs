using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserSubscriptionBs
    {
        private UserSubscriptionDb db;
        public UserSubscriptionBs()
        {
            db = new UserSubscriptionDb();
        }
        public UserSubscriptionBs(bool local)
        {
            if (local)
            {
                db = new UserSubscriptionDb(true);
            }
            else
            {
                db = new UserSubscriptionDb();
            }
        }
        public IEnumerable<UserSubscription> GetAll()
        {
            return db.GetAll();
        }
        public UserSubscription GetByID(long Id)
        {
            return db.GetByID(Id);
        }
        public UserSubscription GetByFeedAndSubID(long FeedId, long SubId)
        {
            return db.GetByFeedAndSubID(FeedId, SubId);
        }
        public IEnumerable<UserSubscription> GetSubsByFeedID(long Id)
        {
            return db.GetSubsByFeedID(Id);
        }

        public IEnumerable<UserSubscription> GetFeedsBySubID(long Id)
        {
            return db.GetFeedsBySubID(Id);
        }

        public IEnumerable<UserSubscription> GetSubsByFeedUserName(string userName)
        {
            return db.GetSubsByFeedUserName(userName);
        }

        public IEnumerable<UserSubscription> GetFeedsBySubUserName(string userName)
        {
            return db.GetFeedsBySubUserName(userName);
        }

        public int Insert(UserSubscription userSubscription)
        {
            return db.Insert(userSubscription);
        }

        public int Delete(long Id)
        {
            return db.Delete(Id);
        }
    }
}
