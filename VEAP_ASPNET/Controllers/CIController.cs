using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VEAP_ASPNET.Models;
using VEAP_ASPNET.Utils;

namespace VEAP_ASPNET.Controllers
{
    public class CIController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Title = "持续集成";

            dynamic JsonData = new ExpandoObject();
            var projects = DAO.GetProjectByUserId(1);
            JsonData.Projects = Tool.DapperRows2JsonString(projects);
            ViewBag.JsonData = JsonData;

            //ViewBag.debug = Tool.ToJsonString(projects[0]);
            return View();
        }
        
    }
}
