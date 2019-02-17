using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class User
    {
        public User()
        {
            Subscriptions = new List<UserSubscription>();
            Followers = new List<UserSubscription>();
            NewsFeedItems = new List<NewsFeedItem>();
        }
        [Key]
        public long ID { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        [EmailAddress, MaxLength(256), DisplayName("User Name"), Required, Index(IsUnique = true)]
        public string UserName { get; set; }
        [MaxLength(25), Required]
        [JsonIgnore]
        public string Password { get; set; }
        [MaxLength(50), DisplayName("Address Line 1")]
        [JsonIgnore]
        public string AddressLine1 { get; set; }
        [MaxLength(50), DisplayName("Address Line 2")]
        [JsonIgnore]
        public string AddressLine2 { get; set; }
        [MaxLength(55)]
        [JsonIgnore]
        public string Country { get; set; }
        [JsonIgnore]
        public string City { get; set; }
        [MaxLength(2)]
        [JsonIgnore]
        public string State { get; set; }
        [MaxLength(5)]
        [JsonIgnore]
        public string ZipCode { get; set; }
        [DisplayName("Birth Date")]
        [JsonIgnore]
        public DateTime BirthDate { get; set; }
        [JsonIgnore]
        public DateTime SignupDate { get; set; }
        [JsonIgnore]
        public bool AdminAcct { get; set; }
        [JsonIgnore]
        public bool AllowPost { get; set; }
        [Required, MaxLength(60), DataType(DataType.ImageUrl)]
        public string ProfilePic { get; set; }
        [ForeignKey("User_Sub_ID")]
        public List<UserSubscription> Subscriptions { get; set; }
        [ForeignKey("User_Feed_ID")]
        public List<UserSubscription> Followers { get; set; }
        [ForeignKey("UserID")]
        public List<NewsFeedItem> NewsFeedItems { get; set; }
        [ForeignKey("CommentUserID")]
        public List<NewsFeedItemComment> NewsFeedItemComments { get; set; }
    }
}
