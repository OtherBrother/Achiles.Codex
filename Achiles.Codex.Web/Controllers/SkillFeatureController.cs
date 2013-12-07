using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class SkillFeatureController : CodexItemBaseController
    {
        //
        // GET: /SkillFeature/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Edit(string id)
        {
            var model = GetModel<SkillFeature>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<SkillFeature> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
                 
                //set item other properties if neccessary..
                storedItem.MaxLevel = input.CodexItem.MaxLevel;

                DocumentSession.SaveChanges();
                Success("New skill feature is created");
            }
            return View(input);
        }


    }
}