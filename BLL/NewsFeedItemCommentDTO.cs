using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NewsFeedItemCommentDTO
    {
        public long ID { get; set; }
        
        public string Comment_Body { get; set; }
        
        public DateTime CommentDate { get; set; }
        public long CommentUserID { get; set; }
        public long NewsFeedItemID { get; set; }
        
        public UserDTO User { get; set; }
        
        public NewsFeedItemDTO NewsFeedItem { get; set; }
    }
}
