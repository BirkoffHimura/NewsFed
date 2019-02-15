using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class UserData
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress, MaxLength(256), DisplayName("User Name"), Required]
        public string UserName { get; set; } 
        [MaxLength(50), DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }
        [MaxLength(50), DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }
        [MaxLength(55)]
        public string Country { get; set; }
        public string City { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(5)]
        public string ZipCode { get; set; }
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }
        
        public string ProfilePic { get; set; }
        public long Followers { get; set; }
        public long Subscribers { get; set; }
    }
}
