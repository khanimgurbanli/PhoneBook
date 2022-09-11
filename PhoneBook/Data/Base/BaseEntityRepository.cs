using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PhoneBook.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Base
{
    public class BaseEntityRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context=new AppDbContext();
        public bool Add(T entity)
        {
            _context.Set<T>().Add(entity);
            bool result = Save() > 0;
            return result;
        }

        public bool Delete(int id)
        {
            var deleteByNumber = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(deleteByNumber);
            entityEntry.State = EntityState.Deleted;

            bool result = Save() > 0;
            return result;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public List<T> GetById(int id) => _context.Set<T>().Where(x => x.Id == id).ToList();

        public List<T> GetByKey(string search) => _context.Set<T>().Where(x => x.Name == search 
        ||  x.Surname == search || x.Number==search).ToList() ;

        public List<T> GetByNumber(string search) => _context.Set<T>().Where(x => x.Number == search).ToList();

        public int Save()=> _context.SaveChanges();
    }
}
