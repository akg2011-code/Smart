using SmartSite.DAL_Functionality;
using SmartSite.Models;
using SmartSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmartSite.Controllers
{
    public class ProductTypeController : Controller
    {
        ProductTypeDAL DAL;
        ApplicationDbContext context;
        public ProductTypeController()
        {
            DAL = new ProductTypeDAL();
            context = new ApplicationDbContext();
        }

        //------------------- type details -------------------
        public ActionResult ProductTypeDetails(int id)
        {

            ViewData["product"] = DAL.filterProductsByType(id);

            ProductType productType = DAL.GetProductTypeByID(id);
            if (productType != null)
                return View(productType);
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        //[Authorize(Roles = "User")]
        // ------------------ filter products by type -------
        public ActionResult FilterProductsByType(int id) // id = type ID
        {
            IEnumerable<Product> filtereProducts = DAL.filterProductsByType(id);
            return View(filtereProducts);
        }

        // ------------------  all products ---------------
        //[Authorize(Roles = "User")]
        public ActionResult AllProductTypes()
        {
            ViewData["Category"] = new SelectList(context.Category, "ID", "CategoryName");
            List<ProductType> allProductTypes = DAL.GetAllProductTypes().ToList();
            return View(allProductTypes);
        }

        // ------------------ filter product type by category -----------
        //[Authorize(Roles = "User")]
        public ActionResult FilterProductTypeByCategory(int? id) // id = category ID
        {
            if (id == null)
                return View("~/Views/Shared/Error.cshtml");
            ViewBag.category = context.Category.Find(id);
            IEnumerable<ProductType> filteredTypes = DAL.FilterProductTypeByCategory(id);
            return View(filteredTypes);
        }

        [Authorize(Roles = "Admin")]
        // ---------------- create type ------------------
        public ActionResult CreateProductType()
        {
            ViewBag.CategoryID = new SelectList(context.Category, "ID", "CategoryName");
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateProductType(ProductType createdProductType, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(HttpContext.Server.MapPath("~/imageUploads"), file.FileName);
                file.SaveAs(path);
                createdProductType.Image = file.FileName;

                bool successfullyCreatedProductType = DAL.CreateProductType(createdProductType);
                if (successfullyCreatedProductType)
                    return RedirectToAction("FilterProductTypeByCategory", new { id = createdProductType.CategoryID });
                else
                    return View(createdProductType);
            }

            ViewBag.CategoryID = new SelectList(context.Category, "ID", "CategoryName");
            return View(createdProductType);
        }

        [Authorize(Roles = "Admin")]
        // --------------------- edit type ----------------------
        public ActionResult EditProductType(int id)
        {
            ViewData["Category"] = new SelectList(context.Category, "ID", "CategoryName");
            ProductType updatedProductType = DAL.GetProductTypeByID(id);
            if (updatedProductType != null)
                return View(updatedProductType);
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditProductType(ProductType modifiedProductType, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(HttpContext.Server.MapPath("~/imageUploads"), file.FileName);
                file.SaveAs(path);
                modifiedProductType.Image = file.FileName;

                bool successfullyModofiedProductType = DAL.EditProductType(modifiedProductType.ID, modifiedProductType);
                if (successfullyModofiedProductType)
                    return RedirectToAction("FilterProductTypeByCategory",new { id=modifiedProductType.CategoryID});
                else
                    return View(modifiedProductType);

            }
            return View(modifiedProductType);
        }

        [Authorize(Roles = "Admin")]
        // ---------------------- delete Type -------------------
        public ActionResult DeleteProductType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductType deletedProductType = DAL.GetProductTypeByID(id);
            if (deletedProductType != null)
                return View(deletedProductType);
            else
                return View("~/Views/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteProductType(ProductType DeletedProductType)
        {
            int deletedTypeCategoryID = DeletedProductType.CategoryID;
            bool successfullyDeletingType = DAL.DeleteProductType(DeletedProductType.ID);
            if (successfullyDeletingType)
                return RedirectToAction("Index","Home");
            else
                return View(DeletedProductType);
        }

    }
}