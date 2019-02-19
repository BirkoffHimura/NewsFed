using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NewsFeedItemDTO
    {        
        public long ID { get; set; }
        
        public long UserID { get; set; }
        
        public string Title { get; set; }
       
        public string Body { get; set; }
        
        public string Img { get; set; }
       
        public DateTime CreateDate { get; set; }
        public UserDTO User { get; set; }
        
        public List<NewsFeedItemCommentDTO> NewsFeedItemComments { get; set; }
    }
}
