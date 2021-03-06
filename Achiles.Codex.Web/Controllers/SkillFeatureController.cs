﻿using System.Linq;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class SkillFeatureController : CodexItemBaseController
    {
        public ActionResult Index()
        {
            return View(DocumentSession.Query<SkillFeature>().ToArray().OrderBy(x=>x.Name));
        }
        
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(string id)
        {
            var model = GetModel<SkillFeature>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(CodexItemModel<SkillFeature> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
        
                DocumentSession.SaveChanges();
                Success("Old age and treachery will overcome youth and skill");
            }
            return View(input);
        }
    }
}