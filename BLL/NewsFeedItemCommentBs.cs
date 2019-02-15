using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NewsFeedItemCommentBs
    {
        private NewsFeedItemCommentDb db;
        public NewsFeedItemCommentBs()
        {
            db = new NewsFeedItemCommentDb();
        }
        public NewsFeedItemCommentBs(bool local)
        {
            if (local)
            {
                db = new NewsFeedItemCommentDb(true);
            }
            else
            {
                db = new NewsFeedItemCommentDb();
            }
        }
        public IEnumerable<NewsFeedItemComment> GetAll()
        {
            return db.GetAll();
        }
        public NewsFeedItemComment GetByID(long Id)
        {
            return db.GetByID(Id);
        }
        public List<NewsFeedItemComment> GetByUserName(string UserNane)
        {
            return db.GetByUserName(UserNane);
        }

        public int Insert(NewsFeedItemComment newsFeedItemComment)
        {
            return db.Insert(newsFeedItemComment);
        }

        public int Delete(long Id)
        {
            return db.Delete(Id);
        }

        public int Update(NewsFeedItemComment newsFeedItemComment)
        {
            return db.Update(newsFeedItemComment);
        }
    }
}
