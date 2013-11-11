using System.IO;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Models;
using Raven.Client;

namespace Achiles.Codex.Web.Services
{
    public interface IInitDataService
    {
        void InitData();
    }

    public class InitDataService : IInitDataService
    {
        private readonly IDocumentSession _session;

        public InitDataService(IDocumentSession session)
        {
            _session = session;
        }

        public void InitData()
        {
            InitAttributes();
        }

        private void InitAttributes()
        {
            var strength = new AttributeInfo {Id = "str", Name = "Strength", Order = 1};
            var stamina = new AttributeInfo { Id = "sta", Name = "Stamina", Order = 2 };
            var dexterity = new AttributeInfo { Id = "dex", Name = "Dexterity", Order = 3 };
            var reflexes = new AttributeInfo { Id = "ref", Name = "Reflexes", Order = 4 };
            var perception = new AttributeInfo { Id = "per", Name = "Perception", Order = 5 };
            var will = new AttributeInfo { Id = "will", Name = "Will", Order = 6 };
           
            _session.Store(strength);
            _session.Store(stamina);
            _session.Store(dexterity);
            _session.Store(reflexes);
            _session.Store(perception);
            _session.Store(will);

            _session.Store(new Article { Author ="AndriusFlea", Description = "Test", Name="Test Article" });
            _session.Store(new Article { Author = "Test", Description = "Heavy article", Name = "Some article" });
            _session.Store(new Rule { Description = "Luck is cool", Name = "Luck" });
            _session.Store(new HandWeapon { Description = "Luck is cool", Name = "Luck", Tags =  { "Test"}});
            _session.Store(new Shield { Description = "This is very big and heavy shield", Name = "Big ass shield", Tags = { "Test" } });
        }
    }
}