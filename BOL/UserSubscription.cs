using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class UserSubscription
    {
        [Key]
        public long ID { get; set; }
        [Required]
        [Index("IX_SubFollow", 1, IsUnique = true)]
        public long User_Sub_ID { get; set; }
        [Required]
        [Index("IX_SubFollow", 2, IsUnique = true)]
        public long User_Feed_ID { get; set; }
        [ForeignKey("User_Sub_ID")]
        public User Sub_ID { get; set; }
        [ForeignKey("User_Feed_ID")]
        public User Feed_ID { get; set; }
    }
}
