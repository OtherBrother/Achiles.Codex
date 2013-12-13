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

namespace Achilles.Codex.Web.Controllers
{
    public class MeleeWeaponController : CodexItemBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles="Contributor")]
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

            DocumentSession.SaveChanges();
            Success("Splendid!", "More death tools ☻");
                
            return View(CreateModel(storedItem));
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult GetJsonWeapons(int? pageSize, int? pageNumber)
        {
            var model = GetWeaponListModel(pageSize, pageNumber);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = model };
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult List(int? pageSize, int? pageNumber) {
            var model = GetWeaponListModel(pageSize, pageNumber);
            return View(model);
        }

        private WeaponListViewModel GetWeaponListModel(int? pageSize, int? pageNumber) {

            var ps = pageSize.GetValueOrDefault(10);
            var pn = pageNumber.GetValueOrDefault(1);
            RavenQueryStatistics stats = null;
            var weapons = DocumentSession.Query<MeleeWeapon>().Statistics(out stats).Skip(ps * (pn - 1)).Take(ps).ToArray();
            var model = new WeaponListViewModel
            {
                Weapons = weapons,
                PageSize = ps,
                Page = pn,
                TotalWeapons = stats.TotalResults
            };
            return model;
        }
	}
}