﻿using BLL;
using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFed.Areas.Common.Controllers
{
    [Authorize(Roles = "A,U")]   
    public class HomeController : Controller
    {
        
        // GET: Common/Home
        public ActionResult Index()
        {            
            return View();
        }
    }
}