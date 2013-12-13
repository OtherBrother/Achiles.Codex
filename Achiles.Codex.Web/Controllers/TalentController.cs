using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class TalentController : CodexItemBaseController
    {
        //
        // GET: /Talent/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Edit(string id)
        {
            var model = GetModel<Talent>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<Talent> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
                //..and save 
                DocumentSession.SaveChanges();
                Success("New talent is created");
            }
            return View(input);
        }


    }
}