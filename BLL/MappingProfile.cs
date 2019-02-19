using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BOL;

namespace BLL
{
    public class MappingProfile : Profile
    {
        private static readonly object InitializedLock = new object();

        public static bool Initialized = false;
        public MappingProfile()
        {
            lock (InitializedLock)
            {
                CreateMap<User, UserDTO>().ReverseMap();
                CreateMap<UserSubscription, UserSubscriptionDTO>().ReverseMap();
                CreateMap<NewsFeedItem, NewsFeedItemDTO>().ReverseMap();
                CreateMap<NewsFeedItemComment, NewsFeedItemCommentDTO>().ReverseMap();

                if (!Initialized)
                {
                    Mapper.Initialize(config => config.AddProfile(this));
                    Initialized = true;
                }
            }
        }
    }
}
