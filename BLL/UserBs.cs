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
    public class UserBs
    {
        private UserDb db;
        private MappingProfile mp;
        public UserBs()
        {
            db = new UserDb();
            mp = new MappingProfile();
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
            mp = new MappingProfile();
        }
        public IEnumerable<UserDTO> GetAll()
        {            
            return Mapper.Map<List<UserDTO>>(db.GetAll());
        }
        public IEnumerable<UserDTO> GetRandom()
        {
            return Mapper.Map<List<UserDTO>>(db.GetRandom());
        }

        public IEnumerable<UserDTO> GetRandom(string userName)
        {
            return Mapper.Map<List<UserDTO>>(db.GetRandom(userName));
        }

        public UserDTO GetByID(long Id)
        {            
            return Mapper.Map<UserDTO>(db.GetByID(Id));
        }
        public UserDTO GetByUserName(string UserNane)
        {
            return Mapper.Map<UserDTO>(db.GetByUserName(UserNane));
        }

        public int Insert(UserDTO user)
        {
            return db.Insert(Mapper.Map<User>(user));
        }

        public int Delete(long Id)
        {            
            return db.Delete(Id);
        }

        public int Update(UserDTO user)
        {
            return db.Update(Mapper.Map<User>(user));
        }        
    }
}
