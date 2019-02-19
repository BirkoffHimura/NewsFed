﻿using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NewsFeedItemDb
    {
        private NFedContext db;
        public NewsFeedItemDb()
        {
            db = new NFedContext();
        }
        public NewsFeedItemDb(bool local)
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
        public IEnumerable<NewsFeedItem> GetAll()
        {
            var ret = db.NewsFeedItems
                .Include(u => u.User)
                .Include(c => c.NewsFeedItemComments.Select(us => us.User))
                .OrderByDescending(o => o.CreateDate)
                .ToList();
            
            return ret;
        }
        public NewsFeedItem GetByID(long Id)
        {
            return db.NewsFeedItems
                .Include(u => u.User)
                .Include(c => c.NewsFeedItemComments.Select(us => us.User))
                .Where(x => x.ID == Id)
                .FirstOrDefault();
        }
        public List<NewsFeedItem> GetByUserName(string userName)
        {
            var ret = db.NewsFeedItems.Where(x => x.User.UserName == userName)
                .Include(u => u.User)
                .Include(c => c.NewsFeedItemComments.Select(us => us.User))
                .OrderByDescending(o => o.CreateDate)
                .ToList();
            return ret;
        }

        public List<NewsFeedItem> Search(string criteria)
        {
            string[] sep = { " ", "," };
            List<string> words = criteria.ToUpper().Split(sep, StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            words.Add(criteria.ToUpper());

            var ret = db.NewsFeedItems.Where(x => words.Any(t=> x.User.UserName.ToUpper().Contains(t)) || words.Any(t=> x.Body.ToUpper().Contains(t)) || words.Any(t=> x.Title.ToUpper().Contains(t)))
                .Include(u => u.User)
                .Include(c => c.NewsFeedItemComments.Select(us => us.User))
                .OrderByDescending(o => o.CreateDate)
                .ToList();
            
            return ret;
        }

        public List<NewsFeedItem> GetNewsFeedItemsFromFeedsBySubscriberUserName(string userName)
        {
            List<long> feeds = (from f in db.UserSubscriptions
                               where f.Sub_ID.UserName == userName
                               select f.User_Feed_ID).ToList<long>();

            var ret = db.NewsFeedItems.Where(x =>feeds.Contains(x.UserID))
                .Include(pu => pu.User)
                .Include(c => c.NewsFeedItemComments.Select(u => u.User))
                .OrderByDescending(o => o.CreateDate)
                .ToList();

            return ret;
        }

        public int Insert(NewsFeedItem newsFeedItem)
        {
            db.Entry(newsFeedItem.User).State = EntityState.Unchanged;
            db.NewsFeedItems.Add(newsFeedItem);
            return Save();
        }

        public int Delete(long Id)
        {
            NewsFeedItem newsFeedItem = GetByID(Id);
            db.NewsFeedItems.Remove(newsFeedItem);
            return Save();
        }

        public int Update(NewsFeedItem newsFeedItem)
        {
            db.Set<NewsFeedItem>().AddOrUpdate(newsFeedItem);
            int i = Save();
            return i;
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
