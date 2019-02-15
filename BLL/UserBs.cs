using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBs
    {
        private UserDb db;
        public UserBs()
        {
            db = new UserDb();
        }
        public UserBs(bool local)
        {
            if (local)
            {
                db = new UserDb(true);
            }
            else
            {
                db = new UserDb();
            }
        }
        public IEnumerable<User> GetAll()
        {
            return db.GetAll();
        }
        public IEnumerable<User> GetRandom()
        {
            return db.GetRandom();
        }

        public IEnumerable<User> GetRandom(string userName)
        {
            return db.GetRandom(userName);
        }

        public User GetByID(long Id)
        {
            return db.GetByID(Id);
        }
        public User GetByUserName(string UserNane)
        {
            return db.GetByUserName(UserNane);
        }

        public int Insert(User user)
        {            
            return db.Insert(user);
        }

        public int Delete(long Id)
        {            
            return db.Delete(Id);
        }

        public int Update(User user)
        {            
            return db.Update(user);
        }        
    }
}
