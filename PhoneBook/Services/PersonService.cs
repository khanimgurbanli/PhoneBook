using PhoneBook.Data.Base;
using PhoneBook.Data.Context;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneBook.Services
{
    public class PersonService : BaseEntityRepository<Person>, IPersconService
    {
        BaseEntityRepository<Person> _entityRepository=new BaseEntityRepository<Person>();

        public void SelectModul()
        {
            again:
            Console.WriteLine("");
            Console.WriteLine("Select one modul");
            Console.WriteLine("\n New phone number -> 'A' \n Number list -> 'L' \n Search number -> 'S' \n Delete number -> 'D'  \nMenu -> 'M'\n Exit -> 'E'");

            char selected = char.Parse(Console.ReadLine());

            bool check = true;
            while (check)
            {
                switch (selected.ToString().ToUpper())
                {
                    case "A":
                        Console.WriteLine("Enter your about info");
                        Console.Write("Name: ");
                        var name = Console.ReadLine();
                        Console.Write("Surname: ");
                        var surname = Console.ReadLine();

                        tryEmail: Console.Write("Email: ");
                        var emial = Console.ReadLine();
                        if (!EmailFormat(emial))
                        {
                            Console.WriteLine("Email format is not valid, try again ");
                            goto tryEmail;
                        }

                        tryPhone: Console.Write("Phone number: ");
                        var number = Console.ReadLine();
                        if (!NumberFormat(number))
                        {
                            Console.WriteLine("Phone number format is not valid, try again ");
                            goto tryPhone;
                        }

                        Console.WriteLine("Save and exit? \nPress 'N' to exit 'Y' to save");
                        string yesOrNo = Console.ReadLine();

                        if (yesOrNo.ToUpper() == "Y")
                        {
                            CreatePhoneBook(name, surname, emial, number);
                            goto again;
                        }
                        else
                            goto again;
                        break;
                    case "L":
                        List();
                        goto again;
                        break;
                    case "S":
                        Console.WriteLine("Enter name, surname or email for searching");
                        Console.Write("Search: ");
                        var search = Console.ReadLine();
                        Select(search.Trim());
                        goto again;
                        break;
                    case "D":
                        Console.WriteLine("Enter number for delete");
                        Console.Write("Number Id: ");
                        var DeleteNum =int.Parse(Console.ReadLine());
                        Delete(DeleteNum);
                        goto again;
                        break;
                    case "M":
                        Console.WriteLine("Got to Menu");
                        goto again;
                        break;
                    case "E":
                        Console.WriteLine("Exciting...");
                        check = false;
                        break;
                }
            }
        }

        public void CreatePhoneBook(string name, string surname, string email, string number)
        {
            var checkNum=_entityRepository.GetByNumber(number);
            if(checkNum.Count>0)
            {
                Console.WriteLine("Number already existing");
                return;
            }  

            bool result = _entityRepository.Add(new Person
            {
                Name = name.Trim().Substring(0, 1).ToUpper() + name.Trim().Substring(1).ToLower(),
                Surname = surname.Trim().Substring(0, 1).ToUpper() + surname.Trim().Substring(1).ToLower(),
                Email = email.Trim().ToLower(),
                Number = number.Trim(),
            });

             
            if (result)
                Console.WriteLine("The number was successfully added to the phone book");
        }

        public void Select(string search)
        {
            search = search.Trim().Substring(0, 1).ToUpper() + search.Trim().Substring(1).ToLower();
            var searchPhone = _entityRepository.GetByKey(search);

            ConsoleDesign(searchPhone);
        }

        public void Delete(int id)
        {
            var checkNum = _entityRepository.GetById(id);
            if (checkNum.Count == 0)
            {
                Console.WriteLine("Not found number");
                return;
            }
            var searchPhone = _entityRepository.Delete(id);

            if (searchPhone)
                Console.WriteLine("The number was successfully deleted");
        }

        public void List()
        {
            var list = _entityRepository.GetAll();
            ConsoleDesign(list);
        }

        public void ConsoleDesign(dynamic data)
        {
            var length = 20;

            var standart = String.Format(new String(' ', length));
            Console.WriteLine(standart);

            Console.WriteLine($"Id {standart} Name {standart} Surname {standart} Email{standart} Phone Number");
            Console.WriteLine(String.Format(new String('-', 120)));

            foreach (var info in data)
            {
                length = info.Email.Length;
                Console.WriteLine($"{info.Id}{standart}{info.Name}{standart}{info.Surname}{standart}{info.Email}{standart}{info.Number} ");
            }
        }

        public bool NumberFormat(string number)
        {
            var result = new Regex(@"^(\+?994|0)(77|51|50|55|70|40|60|12)(\-|)(\d){3}(\-|)(\d){2}(\-|)(\d){2}$");
            var match = result.Match(number);
            return match.Success;
        }

        public bool EmailFormat(string email)
        {
            var result = new Regex(@"^([a-zA-Z]+[a-zA-z.!#$%&'*+-=?^`{|}~]{0,64})+[@]+[a-zA-z-]+[.]+[a-zA-z]+$");
            var macth = result.Match(email);
            return macth.Success;
        }
    }
}
