using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class BodyArmorController : CodexItemBaseController
    {
        //
        // GET: /BodyArmor/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Edit(string id)
        {
            var model = GetModel<BodyArmor>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<BodyArmor> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
                //..and save 
                DocumentSession.SaveChanges();
                Success("New body armor is created");
            }
            return View(input);
        }


    }
}