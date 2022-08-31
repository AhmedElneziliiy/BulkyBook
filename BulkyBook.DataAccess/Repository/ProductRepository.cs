using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var obj = _db.Products.FirstOrDefault(m => m.Id== product.Id);
            if (obj!=null)
            {
                if (product.ImageUrl!=null)
                {
                    obj.ImageUrl = product.ImageUrl;
                }
                obj.ISBN = product.ISBN;
                obj.Price = product.Price;
                obj.Price50 = product.Price50;
                obj.Price100 = product.Price100;
                obj.ListPrice = product.ListPrice;
                obj.Title = product.Title;
                obj.Description = product.Description;
                obj.CategoryId = product.CategoryId;
                obj.Author = product.Author;
                obj.CoverTypeId = product.CoverTypeId;

               
            }
            
        }

    }
}
