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
            var strength = new AttributeInfo { Id = "str", Name = "Strength", Order = 1, Description = "Having high value helps you carry big ass weapons"};
            var stamina = new AttributeInfo { Name = "Stamina", Order = 2 };
            var dexterity = new AttributeInfo { Name = "Dexterity", Order = 3 };
            var reflexes = new AttributeInfo { Id = "ref", Name = "Reflexes", Order = 4 };
            var perception = new AttributeInfo { Id = "per", Name = "Perception", Order = 5 };
            var will = new AttributeInfo { Id = "AttributeInfo/will", Name = "Will", Order = 6 };

            _session.Store(strength);
            _session.Store(stamina);
            _session.Store(dexterity);
            _session.Store(reflexes);
            _session.Store(perception);
            _session.Store(will);

            _session.Store(new Article { Author = "AndriusFlea", Description = "Test", Name = "Test Article" });
            _session.Store(new Article { Author = "Test", Description = "Heavy article", Name = "Some article" });
            _session.Store(new Rule { Description = "Luck is cool", Name = "Luck" });
            _session.Store(new Rule { Description = "You count it", Name = "Vigor" });
            
            _session.Store(new HandWeapon { Description = "Kind of sword", Name = "Broadsword", Tags = { "Test" } });
            _session.Store(new CombatSkill { Description = "Helps you against big ass melee weapons", Name = "Evade", Tags = { "Defense" } });
            _session.Store(new CombatSkill { Description = "", Name = "Sword", Tags = { "Defense" } });
            _session.Store(new HeadArmor { Description = "If this does not help you - nothing can", Name = "Titanium great helm", Tags = { "Defense" } });
            _session.Store(new HandWeapon { Description = "Very very dangerous weapon", Name = "Lucky machete", Tags = { "Test" } });
            _session.Store(new HandWeapon { Description = "Useful for braking big ass shields, but otherwise heavy to carry around", Name = "Big ass axe", Tags = { "Test" } });
            _session.Store(new Shield { Description = "This is very big and heavy shield, but may be broken by big ass axe", Name = "Big ass shield", Tags = { "Test" } });
        }
    }
}