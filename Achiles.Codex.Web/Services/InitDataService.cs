﻿using System.IO;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;
using Raven.Client;

namespace Achilles.Codex.Web.Services
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
            var strength = new AttributeInfo {  Name = "Strength", Order = 1, Description = "Having high value helps you carry big ass weapons"};
            var stamina = new AttributeInfo { Name = "Stamina", Order = 2 };
            var dexterity = new AttributeInfo { Name = "Dexterity", Order = 3 };
            var reflexes = new AttributeInfo {  Name = "Reflexes", Order = 4 };
            var perception = new AttributeInfo {  Name = "Perception", Order = 5 };
            var will = new AttributeInfo { Name = "Will", Order = 6 };

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
            
            _session.Store(new MeleeWeapon { Description = "Kind of sword", Name = "Broadsword", Tags = { "test" } });
            _session.Store(new Article { Author = "AndriusFlea", Description = "Broadsword", Name = "Broadsword", Tags = { "meleeweapon" } });
            _session.Store(new CombatSkill { Description = "Helps you against big ass melee weapons", Name = "Evade", Tags = { "Defense" } });
            _session.Store(new CombatSkill { Description = "", Name = "Sword", Tags = { "Defense" } });
            _session.Store(new Article { Author = "AndriusFlea", Description = "Sword", Name = "Sword", Tags = { "meleeweapon" } });
            _session.Store(new HeadArmor { Description = "If this does not help you - nothing can", Name = "Titanium great helm", Tags = { "defense" } });
            
            _session.Store(new MeleeWeapon { Description = "Very very dangerous weapon", Name = "Lucky machete", Tags = { "test" } });
            _session.Store(new Article { Author = "AndriusFlea", Description = "Lucky machete. Donec nisi arcu, iaculis eget tincidunt nec, sagittis id risus. Nulla ligula risus, accumsan et rutrum in, viverra ut velit. Interdum et malesuada fames ac ante ipsum primis in faucibus.", Name = "Lucky machete", Tags = { "meleeweapon" } });

            _session.Store(new MeleeWeapon { Description = "Useful for braking big ass shields, but otherwise heavy to carry around", Name = "Big ass axe", Tags = { "test" } });
            _session.Store(new Shield { Description = "This is very big and heavy shield, but may be broken by big ass axe", Name = "Big ass shield", Tags = { "test" } });
        }
    }
}