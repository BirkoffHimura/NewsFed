using BLL;
using JsonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFed.Areas.Common.Controllers
{
    [Authorize(Roles = "A,U")]
    public class UserProfileBoxController : Controller
    {
        UserBs uBs;
        public UserProfileBoxController()
        {
            uBs = new UserBs();
        }
        // GET: Common/UserProfileBox
        
        [HttpPost]
        public ActionResult GetInfo()
        {
            var up = uBs.GetByUserName(User.Identity.Name);
            UserData udata = new UserData()
            {
                Name = up.Name,
                UserName = up.UserName,
                ProfilePic = up.ProfilePic,
                Followers = up.Followers.Count(),
                Subscribers = up.Subscriptions.Count()
            };
            JsontResultSet jr = new JsontResultSet
            {
                Data = udata
            };
            return jr;
        }

        [HttpPost]
        public ActionResult GetInfoByID(long ID)
        {
            var up = uBs.GetByID(ID);
            UserData udata = new UserData()
            {
                Name = up.Name,
                UserName = up.UserName,
                ProfilePic = up.ProfilePic,
                Followers = up.Followers.Count(),
                Subscribers = up.Subscriptions.Count()
            };
            JsontResultSet jr = new JsontResultSet
            {
                Data = udata
            };
            return jr;
        }
    }
}