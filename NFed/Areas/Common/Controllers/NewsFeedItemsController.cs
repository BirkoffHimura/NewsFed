using BLL;
using JsonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFed.Areas.Common.Controllers
{
    [Authorize(Roles = "A,U")]
    public class NewsFeedItemsController : Controller
    {
        NewsFeedItemBs nfi;
        public NewsFeedItemsController()
        {
            nfi = new NewsFeedItemBs();
        }
        // GET: Common/NewsFeedItems
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetNewsFeedItems()
        {
            var up = nfi.GetAll();
            JsontResultSet jr = new JsontResultSet
            {
                Data = up
            };
            return jr;
        }
        [HttpPost]
        public ActionResult GetNewsFeedItemsSubscriptions()
        {
            var up = nfi.GetNewsFeedItemsFromFeedsBySubscriberUserName(User.Identity.Name);
            JsontResultSet jr = new JsontResultSet
            {
                Data = up
            };
            return jr;
        }
        [HttpPost]
        public ActionResult GetMyNewsFeedItems()
        {
            var up = nfi.GetByUserName(User.Identity.Name);
            JsontResultSet jr = new JsontResultSet
            {
                Data = up
            };
            return jr;
        }

        [HttpPost]
        public ActionResult GetNewsFeedItemsByUserID(long ID)
        {
            UserBs ubs = new UserBs();
            UserDTO user = ubs.GetByID(ID);
            
            var ret = nfi.GetByUserName(user.UserName);
            JsontResultSet jr = new JsontResultSet
            {
                Data = ret
            };
            return jr;
        }

        [HttpPost]
        public ActionResult ItemSearch(string criteria)
        {
            var ret = nfi.Search(criteria);
            JsontResultSet jr = new JsontResultSet
            {
                Data = ret
            };
            return jr;
        }

        [HttpPost]
        public ActionResult CreateNewsFeedItemComment(long NewsFeedItem_ID, string NewsFeedItem_Comment)
        {
            try
            {
                NewsFeedItemCommentBs cmtBs = new NewsFeedItemCommentBs();
                NewsFeedItemDTO NewsFeedItem_obj = nfi.GetByID(NewsFeedItem_ID);
                UserBs ubs = new UserBs();
                UserDTO user = ubs.GetByUserName(User.Identity.Name);

                NewsFeedItemCommentDTO nfic = new NewsFeedItemCommentDTO()
                {
                    Comment_Body = NewsFeedItem_Comment,
                    CommentDate = DateTime.Now,
                    User = user,
                    CommentUserID = user.ID,
                    NewsFeedItem = NewsFeedItem_obj,
                    NewsFeedItemID = NewsFeedItem_obj.ID
                };

                NewsFeedItemCommentDTO tmpnfic = new NewsFeedItemCommentDTO()
                {
                    Comment_Body = NewsFeedItem_Comment,
                    CommentDate = DateTime.Now,
                    CommentUserID = user.ID,
                    NewsFeedItemID = NewsFeedItem_obj.ID
                };
                cmtBs.Insert(tmpnfic);
                JsontResultSet jr = new JsontResultSet
                {
                    Data = nfic
                };
                return jr;
            }
            catch(Exception ex)
            {
                JsontResultSet jr = new JsontResultSet
                {
                    Data = null 
                };
                return jr;
            }
        }

        [HttpPost]
        public ActionResult CreateNewsFeedItem(string NewsFeedItem_Title, string NewsFeedItem_Body)
        {            
            UserBs uBs = new UserBs();
            UserDTO user = uBs.GetByUserName(User.Identity.Name);
            string fileNam = string.Empty;
            //string file = Request.Files[Request.Files.Count - 1];
            
                var fileContent = Request.Files[Request.Files.Count - 1];
            if (fileContent != null && fileContent.ContentLength > 0)
                {
                    Guid guid = Guid.NewGuid();
                    var inputStream = fileContent.InputStream;
                    var fileName = guid.ToString() + Path.GetExtension(fileContent.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/acct"), fileName);
                    using (var fileStream = System.IO.File.Create(path))
                    {
                        inputStream.CopyTo(fileStream);
                    }
                    fileNam = fileName;
                }
                    
            string errorMessage = "";
            try
            {
                NewsFeedItemDTO n = new NewsFeedItemDTO()
                {
                    Title = NewsFeedItem_Title,
                    Body = NewsFeedItem_Body,
                    CreateDate = DateTime.Now,
                    UserID = user.ID,
                    User = user
                };
                if(fileNam!= string.Empty)
                {
                    n.Img = fileNam;
                }
                nfi.Insert(n);

                var ret = nfi.GetAll();
                JsontResultSet jr = new JsontResultSet
                {
                    Data = ret
                };
                return jr;
            }
            catch (Exception ex)
            {
                if(fileNam!=string.Empty)
                {
                    var path = Path.Combine(Server.MapPath("~/images/acct"), fileNam);
                    System.IO.File.Delete(path);
                }
                errorMessage = ex.Message;
                var ret = nfi.GetAll();
                JsontResultSet jr = new JsontResultSet
                {
                    Data = ret
                };
                return jr;
            }

        }
    }
}