using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEAP_ASPNET.Models;
using VEAP_ASPNET.Utils;

namespace VEAP_ASPNET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Build()
        {
            return View();
        }

        public ActionResult Test()
        {
            return Content("hello");
        }
    }
}
