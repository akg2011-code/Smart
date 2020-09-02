using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSite.DAL_Functionality
{
    public class CategoryDAL
    {
        ApplicationDbContext Context;
        public CategoryDAL()
        {
            Context = new ApplicationDbContext();
        }

        void SaveDBChanges()
        {
            Context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories() => Context.Category.ToList();

        public Category GetCategoryByID(int categoryID) => Context.Category.Find(categoryID);

        public IEnumerable<Product> GetProductsOfSpecificCategory(int categoryID)
        {
            return Context.Product.Where(p=>p.ProductType.Category.ID == categoryID);
        }

        public bool CreateCategory(Category newCategory)
        {
            if(newCategory != null)
            {
                Context.Category.Add(newCategory);
                SaveDBChanges();
                return true;
            }
            return false;
        }

        public bool EditCategory(int categotyID ,Category modifiedCategory)
        {
            Category editedCategory = GetCategoryByID(categotyID);
            if (categotyID == modifiedCategory.ID && editedCategory != null)
            {
                editedCategory.CategoryName = modifiedCategory.CategoryName;
                SaveDBChanges();
                return true;
            }
            return false;
        }

        public bool DeletedCategory(int categoryID)
        {
            Category deletedCategory = GetCategoryByID(categoryID);
            if (deletedCategory != null)
            {
                // --------- deleted all related product types and products ---------------
                DeleteRelatedProductTypesWithCategory(categoryID);

                Context.Category.Remove(deletedCategory);
                SaveDBChanges();
                return true;
            }
            return false;
        }


        void DeleteRelatedProductTypesWithCategory(int categoryID)
        {
            if (GetCategoryByID(categoryID) != null)
            {
                IEnumerable<ProductType> relatedProductTypes = Context.ProductType.Where(t => t.CategoryID == categoryID);
                foreach (var type in relatedProductTypes)
                {
                    // ----------------- delete related products -----------------
                    DeleteRelatedProductsWithType(type.ID);

                    // ----------------- deleted product type ------------
                    Context.ProductType.Remove(type);
                }
                SaveDBChanges();
            }
            
        }

        void DeleteRelatedProductsWithType(int ProductTypeID)
        {
            if (Context.ProductType.Find(ProductTypeID) != null)
            {
                IEnumerable<Product> relatedProducts = Context.Product.Where(p => p.ProductTypeID == ProductTypeID);
                foreach (var product in relatedProducts)
                {
                    Context.Product.Remove(product);
                }
                SaveDBChanges();
            }
        }

    }
}