using BLL;
using JsonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFed.Areas.Common.Controllers
{
    public class MySubscriptionController : Controller
    {
        UserSubscriptionBs usBs;
        UserBs uBs;
        public MySubscriptionController()
        {
            usBs = new UserSubscriptionBs();
            uBs = new UserBs();
        }
        // GET: Common/MySubscription
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetList()
        {
            var up = usBs.GetFeedsBySubUserName(User.Identity.Name);
            JsontResultSet jr = new JsontResultSet
            {
                Data = up
            };
            return jr;
        }
        [HttpPost]
        public ActionResult UnSubscribe(long Id)
        {
            BOL.User user = uBs.GetByUserName(User.Identity.Name);
            BOL.UserSubscription us = usBs.GetByFeedAndSubID(Id, user.ID);
            
            usBs.Delete(us.ID);
            var data = usBs.GetFeedsBySubUserName(User.Identity.Name);
            JsontResultSet jr = new JsontResultSet
            {
                Data = data
            };
            return jr;
        }
    }
}