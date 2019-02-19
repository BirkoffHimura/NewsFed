using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL
{
    public class NewsFeedItemBs
    {
        private NewsFeedItemDb db;
        private MappingProfile mp;
        public NewsFeedItemBs()
        {
            db = new NewsFeedItemDb();
            mp = new MappingProfile();
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
            mp = new MappingProfile();
        }
        public IEnumerable<NewsFeedItemDTO> GetAll()
        {
            return Mapper.Map<List<NewsFeedItemDTO>>(db.GetAll());
        }
        public NewsFeedItemDTO GetByID(long Id)
        {
            return Mapper.Map<NewsFeedItemDTO>(db.GetByID(Id));
        }
        public List<NewsFeedItemDTO> GetByUserName(string userName)
        {
            return Mapper.Map<List<NewsFeedItemDTO>>(db.GetByUserName(userName));
        }

        public List<NewsFeedItemDTO> Search(string criteria)
        {
            return Mapper.Map<List<NewsFeedItemDTO>>(db.Search(criteria));
        }

        public List<NewsFeedItemDTO> GetNewsFeedItemsFromFeedsBySubscriberUserName(string userName)
        {
            return Mapper.Map<List<NewsFeedItemDTO>>(db.GetNewsFeedItemsFromFeedsBySubscriberUserName(userName));
        }

        public int Insert(NewsFeedItemDTO newsFeedItem)
        {
            return db.Insert(Mapper.Map<NewsFeedItem>(newsFeedItem));
        }

        public int Delete(long Id)
        {
            return db.Delete(Id);
        }

        public int Update(NewsFeedItemDTO newsFeedItem)
        {
            return db.Update(Mapper.Map<NewsFeedItem>(newsFeedItem));
        }
    }
}
