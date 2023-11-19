using ListaKontaktow.DataLayer;
using System;

namespace ListaKontaktow.BusinessLayer
{
    public interface IDatabaseManagementService
    {
        void EnsureDatabaseCreation();
    }
     public class DatabaseManagementService : IDatabaseManagementService
    {
        private readonly Func<IContactListDbContext> _contactListDbContextFactoryMethod;

        public DatabaseManagementService(Func<IContactListDbContext> contactListDbContextFactoryMethod)
        {
            _contactListDbContextFactoryMethod = contactListDbContextFactoryMethod;
        }

        public void EnsureDatabaseCreation()
        {
            using (var context = _contactListDbContextFactoryMethod())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
