using SmartSite.DAL_Functionality;
using SmartSite.Models;
using SmartSite.ViewModels;
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
        ApplicationDbContext Context;
        public HomeController()
        {
            DAL = new CategoryDAL();
            Context = new ApplicationDbContext();
        }

        public ActionResult navbarPartial()
        {
            ViewBag.layoutVM = new navbarVM();
            return PartialView("_navLayout", ViewData["layoutVM"]);
        }

        public ActionResult footerPartial()
        {
            ViewBag.layoutVM = new navbarVM();
            return PartialView("_footerLayout", ViewBag.layoutVM.GetLastNews());
        }

        public ActionResult Index()
        {
            IEnumerable<Category> allCategories = DAL.GetAllCategories();


            navbarVM vm = new navbarVM();
            ViewBag.displayedNews = vm.GetLastNews();

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