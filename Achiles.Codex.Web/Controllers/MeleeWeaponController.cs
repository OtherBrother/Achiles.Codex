using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;
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
            storedItem.Reach = input.CodexItem.Reach;
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
                AttackTypes = weapons.SelectMany(x => x.AttackTypes).GroupBy(x => x.Type).Select(x => x.Key).ToArray()

            };

            return View(model);
        }

        public class WeaponListViewModel
        {
            public List<MeleeWeapon> Weapons { get; set; }
            public string[] AttackTypes { get; set; }
        }


    }
}