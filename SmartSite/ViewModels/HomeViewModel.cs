using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSite.ViewModels
{
    public class HomeViewModel
    {
        ApplicationDbContext Context;
        public HomeViewModel()
        {
            Context = new ApplicationDbContext();
        }

        public IEnumerable<Category> GetCategories() => Context.Category.ToList();
        public Category GetSafetyCategory() => Context.Category.FirstOrDefault(c => c.CategoryName == "Safety" || c.CategoryName == "safety" || c.CategoryName =="SAFETY");
    }
}