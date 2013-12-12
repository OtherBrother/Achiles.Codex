using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class EquipmentItemController : CodexItemBaseController
    {
        

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<EquipmentItem> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);

                storedItem.Price = input.CodexItem.Price;
                storedItem.Weight = input.CodexItem.Weight;
                //..and save 

                DocumentSession.SaveChanges();
                Success("New equipment item is created");
            }
            return View(input);
        }


    }
}