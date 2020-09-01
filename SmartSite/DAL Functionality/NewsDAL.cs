using SmartSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSite.DAL_Functionality
{
    public class NewsDAL
    {
        ApplicationDbContext Context;
        public NewsDAL()
        {
            Context = new ApplicationDbContext();
        }

        void SaveDBChanges()
        {
            Context.SaveChanges();
        }

        public IEnumerable<News> GetAllNews() => Context.News.ToList();

        public News GetNewsByID(int newsID) => Context.News.Find(newsID);

        public bool CreateNews(News newNews)
        {
            if (newNews != null)
            {
                Context.News.Add(newNews);
                SaveDBChanges();
                return true;
            }
            return false;
        }

        public bool EditExistedNews(int newsID, News modifiedNews)
        {
            News updatedNews = GetNewsByID(newsID);
            if (updatedNews != null && newsID == modifiedNews.ID)
            {
                updatedNews.Title = modifiedNews.Title;
                updatedNews.Date = modifiedNews.Date;
                updatedNews.Details = modifiedNews.Details;
                updatedNews.Image = modifiedNews.Image;

                SaveDBChanges();
                return true;
            }
            return false;
        }

        public bool DeleteNews(int newsID)
        {
            News DeletedNews = GetNewsByID(newsID);
            if (DeletedNews != null)
            {
                Context.News.Remove(DeletedNews);
                SaveDBChanges();
                return true;
            }
            return false;
        }

    }
}