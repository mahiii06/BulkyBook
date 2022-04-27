using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db) //pass db to base class also
        {
            _db = db;
        }

        /*public void Save()  //shift it to the UnitOfWork
        {
            _db.SaveChanges();
        }*/
         
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
