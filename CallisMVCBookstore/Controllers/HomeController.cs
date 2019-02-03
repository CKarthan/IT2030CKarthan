﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallisMVCBookstore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View("Details");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Calli's Bookstore";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}