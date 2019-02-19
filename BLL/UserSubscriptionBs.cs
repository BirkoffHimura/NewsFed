using AutoMapper;
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
        private MappingProfile mp;
        public UserSubscriptionBs()
        {
            db = new UserSubscriptionDb();
            mp = new MappingProfile();
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
            mp = new MappingProfile();
        }
        public IEnumerable<UserSubscriptionDTO> GetAll()
        {
            return Mapper.Map<List<UserSubscriptionDTO>>(db.GetAll());
        }
        public UserSubscriptionDTO GetByID(long Id)
        {
            return Mapper.Map<UserSubscriptionDTO>(db.GetByID(Id));
        }
        public UserSubscriptionDTO GetByFeedAndSubID(long FeedId, long SubId)
        {
            return Mapper.Map<UserSubscriptionDTO>(db.GetByFeedAndSubID(FeedId, SubId));
        }
        public IEnumerable<UserSubscriptionDTO> GetSubsByFeedID(long Id)
        {
            return Mapper.Map<List<UserSubscriptionDTO>>(db.GetSubsByFeedID(Id));
        }

        public IEnumerable<UserSubscriptionDTO> GetFeedsBySubID(long Id)
        {
            return Mapper.Map<List<UserSubscriptionDTO>>(db.GetFeedsBySubID(Id));
        }

        public IEnumerable<UserSubscriptionDTO> GetSubsByFeedUserName(string userName)
        {
            return Mapper.Map<List<UserSubscriptionDTO>>(db.GetSubsByFeedUserName(userName));
        }

        public IEnumerable<UserSubscriptionDTO> GetFeedsBySubUserName(string userName)
        {
            return Mapper.Map<List<UserSubscriptionDTO>>(db.GetFeedsBySubUserName(userName));
        }

        public int Insert(UserSubscriptionDTO userSubscription)
        {
            return db.Insert(Mapper.Map<UserSubscription>(userSubscription));
        }

        public int Delete(long Id)
        {
            return db.Delete(Id);
        }
    }
}
