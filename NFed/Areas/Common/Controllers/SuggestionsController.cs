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
    public class SuggestionsController : Controller
    {
        UserBs uBs;
        UserSubscriptionBs usBs;
        public SuggestionsController()
        {
            uBs = new UserBs();
            usBs = new UserSubscriptionBs();
        }
        // GET: Common/Suggestions
        [HttpPost]
        public ActionResult GetList()
        {
            var up = uBs.GetRandom(User.Identity.Name);
            JsontResultSet jr = new JsontResultSet
            {
                Data = up
            };
            return jr;
        }
        [HttpPost]
        public ActionResult Subscribe(long Id)
        {
            BOL.User user = uBs.GetByUserName(User.Identity.Name);
            BOL.UserSubscription us = new BOL.UserSubscription()
            {
                User_Feed_ID = Id,
                User_Sub_ID = user.ID
            };
            usBs.Insert(us);
            string[] data = { "sucessful" };
            JsontResultSet jr = new JsontResultSet
            {
                Data = data
            };
            return jr;
        }
    }
}