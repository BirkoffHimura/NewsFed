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
    public class NewsFeedItemCommentBs
    {
        private NewsFeedItemCommentDb db;
        private MappingProfile mp;
        public NewsFeedItemCommentBs()
        {
            db = new NewsFeedItemCommentDb();
            mp = new MappingProfile();
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
            mp = new MappingProfile();
        }
        public IEnumerable<NewsFeedItemCommentDTO> GetAll()
        {
            return Mapper.Map<List<NewsFeedItemCommentDTO>>(db.GetAll());
        }
        public NewsFeedItemCommentDTO GetByID(long Id)
        {
            return Mapper.Map<NewsFeedItemCommentDTO>(db.GetByID(Id));
        }
        public List<NewsFeedItemCommentDTO> GetByUserName(string UserNane)
        {
            return Mapper.Map<List<NewsFeedItemCommentDTO>>(db.GetByUserName(UserNane));
        }

        public int Insert(NewsFeedItemCommentDTO newsFeedItemComment)
        {
            return db.Insert(Mapper.Map<NewsFeedItemComment>(newsFeedItemComment));
        }

        public int Delete(long Id)
        {
            return db.Delete(Id);
        }

        public int Update(NewsFeedItemCommentDTO newsFeedItemComment)
        {
            return db.Update(Mapper.Map<NewsFeedItemComment>(newsFeedItemComment));
        }
    }
}
