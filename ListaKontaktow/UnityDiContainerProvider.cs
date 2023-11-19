using ListaKontaktow.BusinessLayer;
using ListaKontaktow.BusinessLayer.Services;
using ListaKontaktow.DataLayer;
using System;
using Unity;
using Unity.Injection;

namespace ListaKontaktow
{
    public class UnityDiContainerProvider
    {
        public IUnityContainer GetContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IPersonService, PersonService>();
            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IDatabaseManagementService, DatabaseManagementService>();
            container.RegisterType<ICategoriesService, CategoriesService>();

            container.RegisterType<Func<IContactListDbContext>>(new InjectionFactory(ctx => new Func<IContactListDbContext>(() => new ContactListDbContext())));

            return container;
        }
    }
}
