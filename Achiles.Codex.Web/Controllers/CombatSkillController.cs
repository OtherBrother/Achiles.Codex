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
        public ActionResult Edit(string id)
        {
            var model = GetModel<CombatSkill>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<CombatSkill> input)
        {
            ViewBag.Title = "Edit Combat skill";

            if (!ModelState.IsValid) return View(input);

            //insert or update properties common for all base codex items
            var storedItem = UpsertBaseCodexItem(input);
            storedItem.Features = input.CodexItem.Features;

            DocumentSession.SaveChanges();
            Success("New combat skill is created");

            return View(CreateModel(storedItem));
        }
    }
}