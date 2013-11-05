using System.Web.Mvc;
using Achiles.Codex.Web.Controllers;
using Achiles.Codex.Web.Services;
using Microsoft.Practices.Unity;
using Raven.Client;
using Raven.Client.Document;
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
            container.RegisterType<HomeController>();
        }

        private static IDocumentStore CreateDocumentStore()
        {
            var documentStore = new DocumentStore
            {
                ConnectionStringName = "RavenHQ"
            };
            
            documentStore.Initialize();
            return documentStore;
        }

    }
}