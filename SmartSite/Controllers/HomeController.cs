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
        ApplicationDbContext Context;
        public HomeController()
        {
            DAL = new CategoryDAL();
            Context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            IEnumerable<Category> allCategories = DAL.GetAllCategories();

            IEnumerable<News> selectedNews = Context.News.OrderByDescending(n => n.Date);
            ViewBag.lastNews = selectedNews.FirstOrDefault();
            ViewBag.secondLastNews = selectedNews.Skip(1).FirstOrDefault();


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