using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class ShieldController : CodexItemBaseController
    {
        public ActionResult Index()
        {
            return View(DocumentSession.Query<Shield>().ToArray().OrderBy(x => x.Name));
        }

        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(string id)
        {
            var model = GetModel<Shield>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(CodexItemModel<Shield> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
                storedItem.DefenseBonus = input.CodexItem.DefenseBonus;
                storedItem.HitPoints = input.CodexItem.HitPoints;
                DocumentSession.SaveChanges();
                Success("Alertness and courage are life's shield");
            }
            return View(input);
        }
    }
}