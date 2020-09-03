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
        [ValidateAntiForgeryToken]
        public ActionResult CreateProductType(ProductType createdProductType, HttpPostedFileBase UploadImg)
        {
            if (ModelState.IsValid)
            {
                if (UploadImg != null && UploadImg.ContentLength > 0)
                {
                    // uploading image :
                    string ImgPath = Path.Combine(Server.MapPath("~/imageUploads/TypeImg"), UploadImg.FileName);
                    UploadImg.SaveAs(ImgPath);
                    createdProductType.Image = UploadImg.FileName;
                    context.ProductType.Add(createdProductType);
                    context.SaveChanges();
                    return RedirectToAction("AllProductTypes");

                }
                else
                    ViewBag.Message = "You have not specified a file yet ...";

                //bool successfullyCreatedProductType = DAL.CreateProductType(createdProductType);
                //if (successfullyCreatedProductType)
                //    return RedirectToAction("AllProductTypes");
                //else
                //return View(createdProductType);
            }


            //ViewData["Category"] = new SelectList(context.Category, "ID", "CategoryName");
            ViewBag.CategoryID = new SelectList(context.Category, "ID", "CategoryName", createdProductType.CategoryID);
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
        public ActionResult EditProductType(ProductType modifiedProductType, HttpPostedFileBase UploadImg)
        {
            if (ModelState.IsValid)
            {
                if (UploadImg != null && UploadImg.ContentLength > 0)
                {
                    // uploading image :
                    string ImgPath = Path.Combine(Server.MapPath("~/imageUploads/TypeImg"), UploadImg.FileName);
                    UploadImg.SaveAs(ImgPath);
                    modifiedProductType.Image = UploadImg.FileName;


                    bool successfullyModofiedProductType = DAL.EditProductType(modifiedProductType.ID, modifiedProductType);
                    if (successfullyModofiedProductType)
                        return RedirectToAction("AllProductTypes");
                    else
                        return View(modifiedProductType);

                }
                else
                    ViewBag.Message = "You have not specified a file yet ...";

            }
            return View(modifiedProductType);
        }

        [Authorize(Roles = "Admin")]
        // ---------------------- delete Type -------------------
        public ActionResult DeleteProductType(int id)
        {
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
            bool successfullyDeletingType = DAL.DeleteProductType(DeletedProductType.ID);
            if (successfullyDeletingType)
                return RedirectToAction("AllProductTypes");
            else
                return View(DeletedProductType);
        }

    }
}