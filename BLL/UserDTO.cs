using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserDTO
    {
        public UserDTO()
        {
            Subscriptions = new List<UserSubscriptionDTO>();
            Followers = new List<UserSubscriptionDTO>();
            NewsFeedItems = new List<NewsFeedItemDTO>();
        }
        
        public long ID { get; set; }
        
        public string Name { get; set; }
        
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
       
        public string AddressLine1 { get; set; }
       
        public string AddressLine2 { get; set; }
        
        public string Country { get; set; }
        public string City { get; set; }
       
        public string State { get; set; }
        
        public string ZipCode { get; set; }
        
        public DateTime BirthDate { get; set; }
        public DateTime SignupDate { get; set; }
        public bool AdminAcct { get; set; }
        public bool AllowPost { get; set; }
        
        public string ProfilePic { get; set; }
        
        public List<UserSubscriptionDTO> Subscriptions { get; set; }
        
        public List<UserSubscriptionDTO> Followers { get; set; }
        
        public List<NewsFeedItemDTO> NewsFeedItems { get; set; }
        
        public List<NewsFeedItemCommentDTO> NewsFeedItemComments { get; set; }
    }
}
