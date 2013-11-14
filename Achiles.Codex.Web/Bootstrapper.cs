using System.Configuration;
using System.Web.Mvc;
using Achiles.Codex.Web.Controllers;
using Achiles.Codex.Web.Indexes;
using Achiles.Codex.Web.Models;
using Achiles.Codex.Web.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Unity.Mvc4;

namespace Achiles.Codex.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterInstance<IDocumentStore>(CreateDocumentStore(), new ContainerControlledLifetimeManager());
            container.RegisterType<IDocumentSession>(new HierarchicalLifetimeManager(), new InjectionFactory(c => c.Resolve<IDocumentStore>().OpenSession()));
            container.RegisterType<IInitDataService, InitDataService>();
            
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<IUserStore<ApplicationUser>, RavenUserStore>();
        }

        public static void RegisterControllers(IUnityContainer container)
        {
            container.RegisterType<HomeController>();
            container.RegisterType<AccountController>();
        }

        private static IDocumentStore CreateDocumentStore()
        {
            var documentStore = new DocumentStore
            {
                Url = ConfigurationManager.AppSettings["CodexDbConnectionString"],
                ApiKey = ConfigurationManager.AppSettings["CodexDbRavenKey"],
                DefaultDatabase = "Personal-Codex"
                };
            documentStore.Initialize();
            IndexCreation.CreateIndexes(typeof(TagStatisticsIndex).Assembly, documentStore);
            
            return documentStore;
            
        }

    }
}