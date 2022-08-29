using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositry.IRepository
{
    public interface IRepository<T> where T :class
    {
        T Get(int id);
        IEnumerable<T> GetAll( 
            Expression<Func<T,bool>> filter=null, //for empty method retrive all
            Func<IQueryable<T>,IOrderedQueryable<T>>orderBy=null, //range
            string includeProperties=null  //multiable tables
            );
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null, //for empty method retrive all
            string includeProperties = null  //multiable tables
            );
        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);



    }
}
