using PhoneBook.Data.Base;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services
{
    public interface IPersconService: IEntityBaseRepository<Person>
    {
        void SelectModul();
        void CreatePhoneBook(string name, string surname, string email, string number);
        void Select(string search);
        void Delete(int number);
        void List();
        void ConsoleDesign(dynamic data);
        bool NumberFormat(string number);
        bool EmailFormat(string email);
    }
}
