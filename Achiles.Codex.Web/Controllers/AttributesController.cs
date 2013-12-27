using System.Linq;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Services;
using Raven.Client;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class AttributesController : CodexItemBaseController
    {
        public ActionResult Index(bool deleted = false)
        {
            ViewBag.Title = "Attributes";
            var model = DocumentSession.Query<AttributeInfo>().ToArray().OrderBy(a=>a.Order);

            if (deleted)
                Success("Oh well..","Attribute is gone now..");

            return View(model);
        }
        
        [HttpGet]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(string id)
        {
            ViewBag.Title = "Edit melee Attribute";
            var model = GetModel<AttributeInfo>(id);
            return View(model);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(CodexItemModel<AttributeInfo> input)
        {
            ViewBag.Title = "Edit melee Attribute";

            if (!ModelState.IsValid) return View(input);

            var storedItem = UpsertBaseCodexItem(input);
            storedItem.Order = input.CodexItem.Order;
            
            DocumentSession.SaveChanges();
            Success("Saved.", "No, really, it is saved.");
            
            return View(CreateModel(storedItem));

        }
        [Authorize(Roles = "Contributor")]
        public ActionResult Delete(string id)
        {
            var entity = DocumentSession.Load<AttributeInfo>(id);
            if (entity != null)
            {
                DocumentSession.Delete(entity);
                DocumentSession.SaveChanges();
            }
            return RedirectToAction("Index", new { Deleted = true});
        }
    }
}
