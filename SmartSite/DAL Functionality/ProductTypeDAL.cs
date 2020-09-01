using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSite.DAL_Functionality
{
    public class ProductTypeDAL
    {
        ApplicationDbContext Context;
        public ProductTypeDAL()
        {
            Context = new ApplicationDbContext();
        }

        void SaveDBChanges()
        {
            Context.SaveChanges();
        }

        public ProductType GetProductTypeByID(int productTypeID) => Context.ProductType.Find(productTypeID);

        public IEnumerable<ProductType> GetAllProductTypes() => Context.ProductType.ToList();

        public bool CreateProductType(ProductType newProductType)
        {
            if (newProductType != null)
            {
                Context.ProductType.Add(newProductType);
                SaveDBChanges();
                return true;
            }
            return false;
        }

        public bool EditProductType(int productTypeID, ProductType ModifiedProductType)
        {
            if (ModifiedProductType != null && productTypeID == ModifiedProductType.ID)
            {
                ProductType updatedProductType = GetProductTypeByID(productTypeID);
                updatedProductType.Type = ModifiedProductType.Type;
                updatedProductType.CategoryID = ModifiedProductType.CategoryID;
                SaveDBChanges();
                return true;
            }
            return false;
        }

        public bool DeleteProductType(int productTypeID)
        {
            ProductType deletedProductType = GetProductTypeByID(productTypeID);
            if (deletedProductType != null)
            {
                deleteAllProductsForSpecificType(productTypeID);
                Context.ProductType.Remove(deletedProductType);
                SaveDBChanges();
                return true;
            }
            return false;
        }

        void deleteAllProductsForSpecificType(int productTypeID)
        {
            if (GetProductTypeByID(productTypeID) != null)
            {
                IEnumerable<Product> productsForSpecificType = Context.Product.Where(p => p.ProductTypeID == productTypeID);
                if (productsForSpecificType != null && productsForSpecificType.Count() > 1)
                {
                    foreach (var product in productsForSpecificType)
                    {
                        Context.Product.Remove(product);
                        SaveDBChanges();
                    }
                }
            }
        }


    }
}