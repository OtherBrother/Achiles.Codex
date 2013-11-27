using System.Configuration;
using System.Web.Mvc;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Controllers;
using Achiles.Codex.Web.Indexes;
using Achiles.Codex.Web.Models;
using Achiles.Codex.Web.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using Raven.Client;
using Raven.Client.Connection;
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
            Resources.CodexItemLabels.ResourceManager.IgnoreCase = true;

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
            container.RegisterType<ICodexSearchService, CodexSearchService>();
            
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

            documentStore.Conventions.RegisterIdConvention<Article>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<RuleSet>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<Rule>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<AttributeInfo>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<CombatSkill>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<Talent>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<Skill>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<SkillFeature>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<MeleeWeapon>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<RangedWeapon>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<Shield>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<MiscellaneousItem>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<Ammo>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<HeadArmor>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<BodyArmor>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<ArmArmor>(GenerateCodexId);
            documentStore.Conventions.RegisterIdConvention<LegArmor>(GenerateCodexId);

            documentStore.Initialize();
            IndexCreation.CreateIndexes(typeof(TagStatisticsIndex).Assembly, documentStore);
            
            return documentStore;
            
        }
        
        private static string GenerateCodexId<T>(string dbName, IDatabaseCommands commands, T entity) where  T : CodexItem
        {
            return string.Format("{0}/{1}", typeof (T).Name.ToLower(), entity.Name.Trim().Replace(' ', '-').ToLower());
        }
    }
}