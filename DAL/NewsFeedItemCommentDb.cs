using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NewsFeedItemCommentDb
    {
        private NFedContext db;
        public NewsFeedItemCommentDb()
        {
            db = new NFedContext();
        }
        public NewsFeedItemCommentDb(bool local)
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
        public IEnumerable<NewsFeedItemComment> GetAll()
        {
            return db.NewsFeedItemComments
                .Include(u => u.User)
                .Include(n => n.NewsFeedItem)
                .OrderByDescending(o => o.CommentDate)
                .ToList();
        }
        public NewsFeedItemComment GetByID(long Id)
        {
            return db.NewsFeedItemComments
                .Include(u => u.User)
                .Include(n => n.NewsFeedItem)
                .Where(x => x.ID == Id)
                .FirstOrDefault();
        }
        public List<NewsFeedItemComment> GetByUserName(string UserNane)
        {
            return db.NewsFeedItemComments.Where(x => x.User.UserName == UserNane)
                .Include(u => u.User)
                .Include(n => n.NewsFeedItem)
                .OrderByDescending(o => o.CommentDate)
                .ToList();
        }

        public int Insert(NewsFeedItemComment newsFeedItemComment)
        {
            db.Entry(newsFeedItemComment).State = EntityState.Added;
            if (newsFeedItemComment.NewsFeedItem != null)
            {
                db.Entry(newsFeedItemComment.NewsFeedItem).State = EntityState.Unchanged;
            }
            if (newsFeedItemComment.NewsFeedItem != null)
            {
                db.Entry(newsFeedItemComment.User).State = EntityState.Unchanged;
            }

            db.NewsFeedItemComments.Add(newsFeedItemComment);
            int i = Save();
            
            return i;
        }

        public int Delete(long Id)
        {
            NewsFeedItemComment newsFeedItemComment = GetByID(Id);            

            db.NewsFeedItemComments.Remove(newsFeedItemComment);
            return Save();
        }

        public int Update(NewsFeedItemComment newsFeedItemComment)
        {
            db.Entry(newsFeedItemComment).State = System.Data.Entity.EntityState.Modified;
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
