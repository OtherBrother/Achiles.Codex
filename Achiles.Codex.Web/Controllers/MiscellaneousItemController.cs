using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class MiscellaneousItemController : EquipmentItemController
    {
        //
        // GET: /MiscellaneousItem/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Edit(string id)
        {
            var model = GetModel<MiscellaneousItem>(id);
            return View(model);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<MiscellaneousItem> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
                //..and save 
                DocumentSession.SaveChanges();
                Success("New miscellaneous item is created");
            }
            return View(input);
        }


    }
}