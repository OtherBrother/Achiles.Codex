using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;


namespace Achilles.Codex.Web.Controllers
{
    public class RuleController : CodexItemBaseController
    {
        //
        // GET: /Rule/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(string id)
        {
            var model = GetModel<Rule>(id);
            return View(model);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<Rule> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
                //..and save 
                DocumentSession.SaveChanges();
                Success("New rule item is created");
            }
            return View(input);
        }

	}
}