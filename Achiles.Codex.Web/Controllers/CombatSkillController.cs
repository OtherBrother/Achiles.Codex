using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class CombatSkillController : CodexItemBaseController
    {
        //
        // GET: /CombatSkill/
        public ActionResult Index()
        {
            return View();
        }

        private void SetTitle(CodexItemModel<CombatSkill> model)
        {
            ViewBag.Title = model.IsNew ? "New Combat skill" : string.Format("Edit {0}", model.CodexItem.Name);
        }

        public ActionResult Edit(string id)
        {
            var model = GetModel<CombatSkill>(id);
            SetTitle(model);
            
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<CombatSkill> input)
        {

            SetTitle(input);
            if (!ModelState.IsValid) return View(input);

            var storedItem = UpsertBaseCodexItem(input);
            storedItem.Features = input.CodexItem.Features;

            DocumentSession.SaveChanges();
            Success("New combat skill is created");

            return View(CreateModel(storedItem));
        }
    }
}