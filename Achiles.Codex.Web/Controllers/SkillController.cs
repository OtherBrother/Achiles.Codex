using Achiles.Codex.Model;
using Achiles.Codex.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Achiles.Codex.Web.Controllers
{
    public class SkillController : CodexItemBaseController
    {
        //
        // GET: /Skill/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            var model = GetModel<Skill>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<Skill> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
                //set item other properties if neccessary..
                storedItem.Category = input.CodexItem.Category;
                //..and save 
                DocumentSession.SaveChanges();
                Success("New skill is created");
            }
            return View(input);
        }

	}
}