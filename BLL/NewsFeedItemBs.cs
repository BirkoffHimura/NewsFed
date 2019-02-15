using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NewsFeedItemBs
    {
        private NewsFeedItemDb db;
        public NewsFeedItemBs()
        {
            db = new NewsFeedItemDb();
        }
        public NewsFeedItemBs(bool local)
        {
            if (local)
            {
                db = new NewsFeedItemDb(true);
            }
            else
            {
                db = new NewsFeedItemDb();
            }
        }
        public IEnumerable<NewsFeedItem> GetAll()
        {
            return db.GetAll();
        }
        public NewsFeedItem GetByID(long Id)
        {
            return db.GetByID(Id);
        }
        public List<NewsFeedItem> GetByUserName(string userName)
        {
            return db.GetByUserName(userName);
        }

        public List<NewsFeedItem> Search(string criteria)
        {
            return db.Search(criteria);
        }

        public List<NewsFeedItem> GetNewsFeedItemsFromFeedsBySubscriberUserName(string userName)
        {
            return db.GetNewsFeedItemsFromFeedsBySubscriberUserName(userName);
        }

        public int Insert(NewsFeedItem newsFeedItem)
        {
            return db.Insert(newsFeedItem);
        }

        public int Delete(long Id)
        {
            return db.Delete(Id);
        }

        public int Update(NewsFeedItem newsFeedItem)
        {
            return db.Update(newsFeedItem);
        }
    }
}
