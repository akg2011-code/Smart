using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSite.DAL_Functionality
{
    public class ProductDAL
    {
        ApplicationDbContext Context;
        public ProductDAL()
        {
            Context = new ApplicationDbContext();
        }

        void SaveDBChanges()
        {
            Context.SaveChanges();
        }


        public IEnumerable<Product> GetAllProducts() => Context.Product.ToList();

        public Product GetProductByID(int productID) => Context.Product.Find(productID);

        public IEnumerable<Product> GetProuctsByTypeID(int productTypeID) => Context.Product.Where(p => p.ProductTypeID == productTypeID);

        public bool CreateProduct(Product newProduct)
        {
            if (newProduct != null)
            {
                Context.Product.Add(newProduct);
                SaveDBChanges();
                return true;
            }
            return false;
        }

        public bool EditProduct(int productID, Product modifiedProduct)
        {
            if (modifiedProduct != null && productID == modifiedProduct.ID)
            {
                Product updatedProduct = GetProductByID(productID);
                updatedProduct.Name = modifiedProduct.Name;
                updatedProduct.Description = modifiedProduct.Description;
                updatedProduct.PdfFile = modifiedProduct.PdfFile;
                updatedProduct.Image = modifiedProduct.Image;
                updatedProduct.ProductTypeID = modifiedProduct.ProductTypeID;
                updatedProduct.Brand = modifiedProduct.Brand;

                SaveDBChanges();
                return true;
            }
            return false;
        }


        public bool DeleteProduct(int productID)
        {
            Product deletedProduct = GetProductByID(productID);
            if (deletedProduct != null)
            {
                Context.Product.Remove(deletedProduct);
                SaveDBChanges();
                return true;
            }
            return false;
        }

    }
}