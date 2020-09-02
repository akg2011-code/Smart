using SmartSite.DAL_Functionality;
using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSite.Controllers
{
    public class NewsController : Controller
    {
        NewsDAL DAL;
        public NewsController()
        {
            DAL = new NewsDAL();
        }
        public ActionResult GetAllNews()
        {
            IEnumerable<News> allNews = DAL.GetAllNews().ToList();
            return View(allNews);
        }

        public ActionResult NewsDetails(int id) // id = news ID
        {
            News news = DAL.GetNewsByID(id);
            if(news != null)
                return View(news);
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateNews()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateNews(News createdNews, HttpPostedFileBase UploadImg)
        {
            if (ModelState.IsValid)
            {
                string ImgPath = Path.Combine(Server.MapPath("~/imageUploads/NewsImg"), UploadImg.FileName);
                UploadImg.SaveAs(ImgPath);
                createdNews.Image = UploadImg.FileName;

                bool successfullyCreatingNews = DAL.CreateNews(createdNews);
                if(successfullyCreatingNews)
                    return RedirectToAction("GetAllNews");
                else
                    return View(createdNews);

            }
            return View(createdNews);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditNews(int id) // id = news ID
        {
            News modifiedNews = DAL.GetNewsByID(id);
            if (modifiedNews != null)
                return View(modifiedNews);
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditNews(News EditedNews, HttpPostedFileBase UploadImg)
        {
            if (ModelState.IsValid)
            {
                var filepath = UploadImg.FileName;
                System.IO.File.Delete(filepath);

                string ImgPath = Path.Combine(Server.MapPath("~/imageUploads/NewsImg"), UploadImg.FileName);
                UploadImg.SaveAs(ImgPath);
                EditedNews.Image = UploadImg.FileName;

                bool successfullyEditingNews = DAL.EditExistedNews(EditedNews.ID, EditedNews);
                if (successfullyEditingNews)
                    return RedirectToAction("GetAllNews");
                else
                    return View(EditedNews);

            }
            return View(EditedNews);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteNews(int id) // id = news ID
        {
            News deletedNews = DAL.GetNewsByID(id);
            if(deletedNews != null)
                return View(deletedNews);
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteNews(News deletedNews)
        {
            bool successfullyDeletingNew = DAL.DeleteNews(deletedNews.ID);
            if (successfullyDeletingNew)
                return RedirectToAction("GetAllNews");
            else
                return View(deletedNews);
        }
    }
}