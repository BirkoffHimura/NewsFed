using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class NewsFeedItem
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public long UserID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500), Required]
        public string Body { get; set; }
        [MaxLength(60), DataType(DataType.ImageUrl)]
        public string Img { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public User User { get; set; }
        [ForeignKey("NewsFeedItemID")]
        public List<NewsFeedItemComment> NewsFeedItemComments { get; set; }
    }
}
