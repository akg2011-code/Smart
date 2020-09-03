using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSite.ViewModels
{
    public class navbarVM
    {
        ApplicationDbContext Context;
        public navbarVM()
        {
            Context = new ApplicationDbContext();
        }

        public IEnumerable<Category> GetCategories() => Context.Category.ToList();

        public IEnumerable<ProductType> GetProductTypesForSpecificCategory(int categoryID) => Context.ProductType.Where(t=>t.CategoryID==categoryID);

        IEnumerable<News> selectedNews() => Context.News.OrderByDescending(n => n.Date);

        public List<News> GetLastNews()
        {
            List<News> displayedNews = new List<News>()
            {
                selectedNews().FirstOrDefault(),
                selectedNews().Skip(1).FirstOrDefault()
            };

            return displayedNews;
        }


    }
}