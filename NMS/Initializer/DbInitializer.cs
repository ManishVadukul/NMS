using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NMS.Data;
using NMS.Models;
using System;
using System.Linq;

// IDbInitializer is for migrate full database design on your SQL server If you don't have. Don't forgot to change SQL datasource in appsetting.json
namespace NMS.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;      
        


        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
         
        }

        /// <summary>
        /// Initialize will create database and add categories for first time when you run.
        /// </summary>
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();

                    IList<Category> categories = new List<Category>();

                    categories.Add(new Category() {Title = "Personal"});
                    categories.Add(new Category() {Title = "Work"});
                    categories.Add(new Category() {Title = "Holiday"});


                    _db.Categories.AddRange(categories);
                    _db.SaveChanges();
                    

                    


                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

          
           

        }
    }
}
