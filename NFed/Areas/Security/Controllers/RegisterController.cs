using BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFed.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        // GET: Security/Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDTO user, HttpPostedFileBase fileUpload)
        {

            try
            {
                string fileNam = string.Empty;
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    try
                    {
                        fileNam = Guid.NewGuid().ToString();
                        fileNam += Path.GetExtension(fileUpload.FileName);
                        string path = Path.Combine(Server.MapPath("~/images/acct"),
                                                   fileNam);
                        fileUpload.SaveAs(path);
                    }
                    catch (Exception ex)
                    {
                        TempData["msg"] = "ERROR:" + ex.Message.ToString();
                        return RedirectToAction("Index");
                    }
                }


                UserBs uDb = new UserBs();
                user.AdminAcct = false;
                user.AllowPost = false;
                user.SignupDate = DateTime.Now;
                if (fileNam == string.Empty)
                {
                    user.ProfilePic = "noimage.png";
                }
                else
                {
                    user.ProfilePic = fileNam;
                }

                int i = uDb.Insert(user);
                if (i < 0)
                {
                    TempData["msg"] = "<strong id=\"myErrorMessage\">Register Failed!</strong>";
                }
                else
                {
                    TempData["msg"] = "<strong id=\"myErrorMessage\">Account registered successfully!!</strong>";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "<strong id=\"myErrorMessage\">Register Failed!</strong> <i>" + ex.Message + "</i>";
            }
            return RedirectToAction("Index");
        }
    }
}