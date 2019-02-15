﻿using BLL;
using BOL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFed.Areas.Security.Controllers
{
    [Authorize(Roles = "A,U")]
    public class ProfileController : Controller
    {
        // GET: Security/Profile
        public ActionResult Index()
        {
            UserBs ubs = new UserBs();
            User user = ubs.GetByUserName(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user, HttpPostedFileBase fileUpload)
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

                int i = uDb.Update(user);
                if (i < 0)
                {
                    TempData["msg"] = "<strong id=\"myErrorMessage\">Update Failed!</strong>";
                }
                else
                {
                    TempData["msg"] = "<strong id=\"myErrorMessage\">Account Updated successfully!!</strong>";
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