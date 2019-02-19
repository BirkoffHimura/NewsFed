using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserSubscriptionDTO
    {
       
        public long ID { get; set; }
        
        public long User_Sub_ID { get; set; }
        
        public long User_Feed_ID { get; set; }
        
        public UserDTO Sub_ID { get; set; }
        
        public UserDTO Feed_ID { get; set; }
    }
}
