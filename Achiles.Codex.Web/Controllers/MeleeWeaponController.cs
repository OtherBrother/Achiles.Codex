using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;

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

            DocumentSession.SaveChanges();
            Success("Splendid!", "More death tools ☻");
                            return View(CreateModel(storedItem));
        }
	}
}