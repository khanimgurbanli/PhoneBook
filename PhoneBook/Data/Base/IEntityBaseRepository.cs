using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {
        IEnumerable<T> GetAll();
        List<T> GetByKey(string search);
        List<T> GetById(int id);
        List<T> GetByNumber(string search);
        bool Add(T entity);
        bool Delete(int id);
        int Save();
    }
}
