using SmartSite.DAL_Functionality;
using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSite.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDAL DAL;
        public CategoryController()
        {
            DAL = new CategoryDAL();
        }

        public ActionResult GetAllCategories()
        {
            List<Category> allCategories = DAL.GetAllCategories().ToList();
            return View(allCategories);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateCategory(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                bool successfullyCreatingCategry = DAL.CreateCategory(newCategory);
                if (successfullyCreatingCategry)
                    return RedirectToAction("GetAllCategories");
                else
                    return View(newCategory);
            }
            else
                return View(newCategory);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditCategory(int id) // id = category ID
        {
            if (DAL.GetCategoryByID(id) != null)
                return View(DAL.GetCategoryByID(id));
            else
                return View("~/View/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditCategory(Category modifiedCategory)
        {
            if (ModelState.IsValid)
            {
                bool successfullyEditingCategory = DAL.EditCategory(modifiedCategory.ID,modifiedCategory);
                if (successfullyEditingCategory)
                    return RedirectToAction("GetAllCategories");
                else
                    return View(modifiedCategory);
            }
            else
                return View(modifiedCategory);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCategory(int id) // id = category ID
        {
            if (DAL.GetCategoryByID(id) != null)
                return View(DAL.GetCategoryByID(id));
            else
                return View("~/View/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteCategory(Category deletedCategory)
        {
            bool successfullyDeletingCategory = DAL.DeletedCategory(deletedCategory.ID);
            if (successfullyDeletingCategory)
                return RedirectToAction("GetAllCategories");
            else
                return View(deletedCategory);
        }
    }
}