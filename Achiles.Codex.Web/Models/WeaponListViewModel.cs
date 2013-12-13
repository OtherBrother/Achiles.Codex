using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achilles.Codex.Model;

namespace Achilles.Codex.Web.Models
{
    public class WeaponListViewModel
    {
        public IEnumerable<MeleeWeapon> Weapons { get; set; }
        public int TotalWeapons { get; set; }
    }
}