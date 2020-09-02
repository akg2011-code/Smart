using SmartSite.DAL_Functionality;
using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SmartSite.Controllers
{
    public class HomeController : Controller
    {
        CategoryDAL DAL;
        public HomeController()
        {
            DAL = new CategoryDAL();
        }

        public ActionResult Index()
        {
            IEnumerable<Category> allCategories = DAL.GetAllCategories();
            return View(allCategories);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}