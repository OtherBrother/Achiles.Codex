using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;
using Microsoft.Practices.ObjectBuilder2;
using Raven.Client;
using Raven.Abstractions.Extensions;
using Achilles.Codex.Web.Misc;
using WebGrease.Extensions;

namespace Achilles.Codex.Web.Controllers
{
    public class MeleeWeaponController : CodexItemBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Edit melee weapon";

            var model = GetModel<MeleeWeapon>(id);
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(CodexItemModel<MeleeWeapon> input)
        {
            ViewBag.Title = "Edit melee weapon";

            if (!ModelState.IsValid) return View(input);

            var storedItem = UpsertBaseCodexItem(input);

            storedItem.Properties = input.CodexItem.Properties.Distinct().ToList();
            storedItem.Price = input.CodexItem.Price;
            storedItem.ReachMin = input.CodexItem.ReachMin;
            storedItem.ReachMax = input.CodexItem.ReachMax;
            storedItem.AttackTypes = input.CodexItem.AttackTypes;
            storedItem.Weight = input.CodexItem.Weight;
            storedItem.MinimumStrenght = input.CodexItem.MinimumStrenght;

            DocumentSession.SaveChanges();
            Success("Splendid!", "More death tools ☻");

            return View(CreateModel(storedItem));
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetJsonWeapons()
        {
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = DocumentSession.Query<MeleeWeapon>().ToArray() };
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult List()
        {
            ViewBag.Title = "Melee weapons";
            var weapons = DocumentSession.Query<MeleeWeapon>().ToList();
            var model = new WeaponListViewModel
            {

                Weapons = weapons,
                AttackTypes = weapons.SelectMany(x => x.AttackTypes ?? new List<AttackType>()).GroupBy(x => x.Type).Select(x => x.Key).ToArray(),
                AvailableTags =  weapons.SelectMany(x=>x.Tags).Distinct().OrderBy(x=>x).ToArray(),
                SelectedTags = new string[0],
                SelectedAttacks = new string[0]
            };

            return View(model);
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult List(string [] tags, string[] attacks)
        {
            ViewBag.Title = "Melee weapons";
            var weapons = DocumentSession.Query<MeleeWeapon>().ToList();
            tags = tags ?? new string[0];
            attacks = attacks ?? new string[0];
            
            var model = new WeaponListViewModel
            {
                Weapons = weapons
                    .Where(x => tags.Length == 0 || x.Tags.Any(y=>tags.Contains(y)))
                    .Where(x => attacks.Length == 0 || x.AttackTypes.Any(y => attacks.Contains(y.Type)))
                    .ToList(),
                AttackTypes = weapons.SelectMany(x => x.AttackTypes ?? new List<AttackType>()).GroupBy(x => x.Type).Select(x => x.Key).ToArray(),
                AvailableTags = weapons.SelectMany(x => x.Tags).Distinct().OrderBy(x => x).ToArray(),
                SelectedTags = tags,
                SelectedAttacks = attacks
            };

            return View(model);
        }

        public class WeaponListViewModel
        {
            public List<MeleeWeapon> Weapons { get; set; }
            public string[] AttackTypes { get; set; }
            public string[] AvailableTags { get; set; }
            public string[] SelectedTags { get; set; }
            public string[] SelectedAttacks { get; set; }
        }
    }
}