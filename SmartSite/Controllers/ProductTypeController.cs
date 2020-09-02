using SmartSite.DAL_Functionality;
using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSite.Controllers
{
    [Authorize(Roles = "Admin")]
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

        // ------------------ filter products by type -------
        public ActionResult FilterProductsByType(int id) // id = type ID
        {
            IEnumerable<Product> filtereProducts = DAL.filterProductsByType(id);
            return View(filtereProducts);
        }

        // ------------------  all products ---------------
        public ActionResult AllProductTypes()
        {
            ViewData["Category"] = new SelectList(context.Category, "ID", "CategoryName");
            List<ProductType> allProductTypes = DAL.GetAllProductTypes().ToList();
            return View(allProductTypes);
        }

        // ------------------ filter product type by category -----------
        public ActionResult FilterProductTypeByCategory(int? id) // id = category ID
        {
            if (id == null)
                return View("~/View/Shared/Error.cshtml");
            IEnumerable<ProductType> filteredTypes = DAL.FilterProductTypeByCategory(id);
            return View(filteredTypes);
        }


        // ---------------- create type ------------------
        public ActionResult CreateProductType()
        {
            ViewData["Category"] = new SelectList(context.Category, "ID", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateProductType(ProductType createdProductType)
        {
            if (ModelState.IsValid)
            {
                bool successfullyCreatedProductType = DAL.CreateProductType(createdProductType);
                if (successfullyCreatedProductType)
                    return RedirectToAction("AllProductTypes");
                else
                    return View(createdProductType);
            }
            return View(createdProductType);
        }

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
        [HttpPost]
        public ActionResult EditProductType(ProductType modifiedProductType)
        {
            if (ModelState.IsValid)
            {
                bool successfullyModofiedProductType = DAL.EditProductType(modifiedProductType.ID, modifiedProductType);
                if (successfullyModofiedProductType)
                    return RedirectToAction("AllProductTypes");
                else
                    return View(modifiedProductType);
            }
            return View(modifiedProductType);
        }

        // ---------------------- delete Type -------------------
        public ActionResult DeleteProductType(int id)
        {
            ProductType deletedProductType = DAL.GetProductTypeByID(id);
            if (deletedProductType != null)
                return View(deletedProductType);
            else
                return View("~/Views/Shared/Error.cshtml");
        }
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