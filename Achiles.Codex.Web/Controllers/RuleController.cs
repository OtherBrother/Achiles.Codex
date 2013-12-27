using System.Linq;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;
using Raven.Client.Linq;

namespace Achilles.Codex.Web.Controllers
{
    public class RuleController : CodexItemBaseController
    {
        [HttpGet]
        public ActionResult Index(Gear? gear)
        {
            var q = DocumentSession.Query<Rule>();
            if (gear.HasValue)
            {
                var g = gear.Value;
                return View(q.Where(x => x.Gear == g));
            }

            return View(q.ToArray());
        }
        
        [Authorize(Roles = "Contributor")]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = GetModel<Rule>(id);
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(CodexItemModel<Rule> input)
        {

            if (ModelState.IsValid)
            {
                var storedItem = UpsertBaseCodexItem(input);
                storedItem.Gear = input.CodexItem.Gear;
                DocumentSession.SaveChanges();
                Success("Nice work!", "Rules are made to be broken!");
            }

            return View(input);
        }
	}
}