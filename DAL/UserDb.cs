using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDb
    {
        private NFedContext db;
        public UserDb()
        {
            db = new NFedContext();
        }
        public UserDb(bool local)
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
        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public IEnumerable<User> GetRandom()
        {
            return db.Users
                .OrderBy(o=> Guid.NewGuid())                
                .Take(8)
                .ToList();
        }

        public IEnumerable<User> GetRandom(string userName)
        {
            List<string> subs = (from s in db.UserSubscriptions
                                 where s.Sub_ID.UserName == userName
                                 select s.Feed_ID.UserName).ToList<string>();
            return db.Users
                .OrderBy(o => Guid.NewGuid())
                .Take(8)
                .Where(x => x.UserName != userName && !subs.Contains(x.UserName))
                .ToList();
        }
        public User GetByID(long Id)
        {
            return db.Users
                .Include(f => f.Followers)
                .Include(s => s.Subscriptions)
                .Where(x => x.ID == Id)
                .FirstOrDefault();
        }
        public User GetByUserName(string UserNane)
        {
            return db.Users.Where(x => x.UserName == UserNane)
                .Include(f => f.Followers)
                .Include(s => s.Subscriptions)
                .FirstOrDefault();
        }

        public int Insert(User user)
        {
            db.Users.Add(user);
            return Save();
        }
        
        public int Delete(long Id)
        {
            User user = GetByID(Id);
            List<UserSubscription> subs = (from s in db.UserSubscriptions
                                          where s.User_Sub_ID == user.ID || s.User_Feed_ID == user.ID
                                          select s).ToList();
            foreach(UserSubscription us in subs)
            {
                db.UserSubscriptions.Remove(us);                
            }            

            List<NewsFeedItemComment> coms = (from c in db.NewsFeedItemComments
                                              where c.CommentUserID == user.ID
                                              select c).ToList();

            foreach (NewsFeedItemComment nfc in coms)
            {
                db.NewsFeedItemComments.Remove(nfc);
            }

            List<NewsFeedItem> news = (from n in db.NewsFeedItems
                                       where n.UserID == user.ID
                                       select n).ToList();

            foreach (NewsFeedItem nfi in news)
            {
                db.NewsFeedItems.Remove(nfi);
            }
            
            db.Users.Remove(user);
            return Save();
        }

        public int Update(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            int i = Save();
            db.Configuration.ValidateOnSaveEnabled = true;
            return i;
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
