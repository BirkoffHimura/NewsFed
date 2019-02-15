using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BOL
{
    public class NewsFeedItemComment
    {
        [Key]
        public long ID { get; set; }
        [Required, MaxLength(200)]
        public string Comment_Body { get; set; }
        [Required]
        public DateTime CommentDate { get; set; }        
        public long CommentUserID { get; set; }
        public long NewsFeedItemID { get; set; }
        [ForeignKey("CommentUserID")]
        public User User { get; set; }
        [ForeignKey("NewsFeedItemID")]
        public NewsFeedItem NewsFeedItem { get; set; }
    }
}
