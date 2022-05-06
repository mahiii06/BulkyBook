using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db) : base(db) //pass db to base class also
        {
            _db = db;
        }

        /*public void Save()  //shift it to the UnitOfWork
        {
            _db.SaveChanges();
        }*/
         
        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
