using ListaKontaktow.DataLayer;
using ListaKontaktow.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaKontaktow.BusinessLayer.Services
{
    public interface IPersonService
    {
        void Add(Person person);
        List<Person> GetAll();
        Person GetPersonByNameAndSurname(string name, string surname);
        void Update(Person person);
        void Delete(Person person);
    }

    public class PersonService : IPersonService
    {
        private readonly Func<IContactListDbContext> _contactListDbContextFactoryMethod;

        public PersonService(Func<IContactListDbContext> contactListDbContextFactoryMethod)
        {
            _contactListDbContextFactoryMethod = contactListDbContextFactoryMethod;
        }

        public void Add(Person person)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }
        }

        public List<Person> GetAll()
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                return context.Persons
                    .Include(p => p.Category)
                    .ToList();
            }
        }

        public Person GetPersonByNameAndSurname(string name, string surname)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                return context.Persons
                    .FirstOrDefault(p => p.Name == name && p.Surname == surname);
            }
        }

        public void Update(Person person)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                context.Persons.Update(person);
                context.SaveChanges();
            }
        }

        public void Delete(Person person)
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                context.Persons.Remove(person);
                context.SaveChanges();
            }
        }
    }
}
