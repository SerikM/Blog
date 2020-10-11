using Data.Aurora.EF;
using Data.Aurora.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Aurora.Interfaces
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntityBase
    {
        private readonly BlogContext _context;
        public BaseRepository(BlogContext context) 
        {
            _context = context;
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetBlogPostsList(int numToTake, int numToSkip)
        {
          return  _context.Set<T>().Skip(numToSkip).Take(numToTake);
        }

        public IQueryable<T> GetNumOfItemsWithInclude(string propertyName)
        {
            throw new NotImplementedException();
        }

        public T GetSingle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
