using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartSite.Models;

namespace SmartSite.Controllers
{
    // class of authodized roles :
    public class AuthorizedRoles :AuthorizeAttribute
    {
        public string roles ="Admin , User";
    }

    // ----------------------------------------------------

    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.ProductType);
            return View(product.ToList());
        }

        [AuthorizedRoles] // authorized class for both two roles
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewData["category"] = db.Product.Find(id).ProductType.Category;
            return View(product);
        }

        // ------------------------ search product bu Name ---------------------------
        public ActionResult SearchProductByName(string productName)
        {
            if (productName==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<Product> products = db.Product.Where(p => p.Name.Contains(productName));
            if (products != null && products.Count() > 0)
                return View("Index", products);
            else
                return View("~/View/Shared/NotFound.cshtml");
        }


        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "Type");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product ,HttpPostedFileBase UploadImg , HttpPostedFileBase UploadPdf)
        {
            if (ModelState.IsValid)
            {
                if (UploadImg != null || UploadPdf != null || UploadImg.ContentLength > 0 || UploadPdf.ContentLength > 0)
                {
                    // uploading image :
                    string ImgPath = Path.Combine(Server.MapPath("~/imageUploads"), UploadImg.FileName);
                    UploadImg.SaveAs(ImgPath);
                    product.Image = UploadImg.FileName;

                    // uploading PDF :
                    string pdfPath = Path.Combine(Server.MapPath("~/pdfUploads"), UploadPdf.FileName);
                    UploadPdf.SaveAs(pdfPath);
                    product.PdfFile = UploadPdf.FileName;

                    db.Product.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ViewBag.Message = "You have not specified a file yet ...";
            }

            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "Type", product.ProductTypeID);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "Type", product.ProductTypeID);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase UploadImg , HttpPostedFileBase UploadPdf)
        {
            if (ModelState.IsValid)
            {
                if (UploadImg != null || UploadPdf != null || UploadImg.ContentLength > 0 || UploadPdf.ContentLength > 0)
                {
                    //string fullPath = Request.MapPath("~/imageUploads" + product.Image);
                    //if (System.IO.File.Exists(fullPath))
                    //{
                    //    System.IO.File.Delete(fullPath);
                    //}
                    // delete file using your filepath (path + filename)
                    var filepath = UploadImg.FileName;
                    System.IO.File.Delete(filepath);

                    // uploading image :
                    string ImgPath = Path.Combine(Server.MapPath("~/imageUploads"), UploadImg.FileName);
                    UploadImg.SaveAs(ImgPath);
                    product.Image = UploadImg.FileName;

                    // uploading PDF :
                    string pdfPath = Path.Combine(Server.MapPath("~/pdfUploads"), UploadPdf.FileName);
                    UploadPdf.SaveAs(pdfPath);
                    product.PdfFile = UploadPdf.FileName;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "Type", product.ProductTypeID);
            return View(product);
        }

        // ------------- download pdf ----------------
        public FileResult Download(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath("~/pdfUploads"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        private string GetFileTypeByExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".docx":
                case ".doc":
                    return "Microsoft Word Document";
                case ".xlsx":
                case ".xls":
                    return "Microsoft Excel Document";
                case ".txt":
                    return "Text Document";
                case ".jpg":
                case ".png":
                    return "Image";
                case ".pdf":
                    return "PDF";
                default:
                    return "Unknown";
            }
        }


        // ---------------- delete product --------------
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
