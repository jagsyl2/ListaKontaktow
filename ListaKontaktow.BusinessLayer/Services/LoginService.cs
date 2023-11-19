using ListaKontaktow.DataLayer;
using ListaKontaktow.DataLayer.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ListaKontaktow.BusinessLayer.Services
{
    public interface ILoginService
    {
        bool IsValidEmail(string email);
        bool IsDuplicateEmail(string email);
        void AddUser(User user);
        Person GetLoginAccess(string email, string password);
    }

    public class LoginService : ILoginService
    {
        private readonly Func<IContactListDbContext> _contactListDbContextFactoryMethod;

        public LoginService(Func<IContactListDbContext> contactListDbContextFactoryMethod)
        {
            _contactListDbContextFactoryMethod = contactListDbContextFactoryMethod;
        }

        public bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        public bool IsDuplicateEmail(string email)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                return context.Users.Any(e => e.Email == email);
            }
        }

        public void AddUser(User user)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public Person GetLoginAccess(string email, string password)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                return context.Persons
                    .AsQueryable()
                    .Where(p => p.Email == email && p.Password == password)
                    .FirstOrDefault();
            }
        }
    }
}
