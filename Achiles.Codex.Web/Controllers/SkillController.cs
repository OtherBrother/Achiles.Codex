using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class SkillController : CodexItemBaseController
    {
        public ActionResult Index()
        {
            return View(DocumentSession.Query<Skill>().ToArray().OrderBy(x => x.Name));
        }

        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(string id)
        {
            var model = GetModel<Skill>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(CodexItemModel<Skill> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);

                DocumentSession.SaveChanges();
                Success("Work is afraid of a skilled worker.");
            }
            return View(input);
        }
    }
}