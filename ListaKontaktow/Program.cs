using ListaKontaktow.BusinessLayer;
using ListaKontaktow.BusinessLayer.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace ListaKontaktow
{
    class Program
    {
        private readonly IDatabaseManagementService _databaseManagementService;
        private readonly ICategoriesService _categoriesService;

        static void Main(string[] args)
        {
            var container = new UnityDiContainerProvider().GetContainer();
            container.Resolve<Program>().Run();

            WebHost.CreateDefaultBuilder()
                .UseUnityServiceProvider(container)
                .ConfigureServices(services =>
                {
                    services.AddMvc();
                    services.AddCors(options =>
                    {
                        options.AddPolicy("AllowOrigin",
                            builder =>
                            {
                                builder.WithOrigins(
                                    "http://localhost:4200"
                                    )
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                            });
                    });
                })
                .Configure(app =>
                {
                    app.UseCors("AllowOrigin");
                    app.UseMvc();

                })
                .UseUrls("http://*:10500")
                .Build()
                .Run();
        }

        public Program(IDatabaseManagementService databaseManagementService,
            ICategoriesService categoriesService)
        {
            _databaseManagementService = databaseManagementService;
            _categoriesService = categoriesService;
        }

        void Run()
        {
            _databaseManagementService.EnsureDatabaseCreation();
            _categoriesService.CreateCategories();
            _categoriesService.CreateSubcategories();
        }
    }
}
