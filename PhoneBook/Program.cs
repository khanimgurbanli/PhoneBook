
using PhoneBook.Services;
namespace PhoneBook
{
    class Program
    {
      
        public static  void Main()
        {
            PersonService service = new PersonService();
            service.SelectModul();
        }
    }
}
