using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Achiles.Codex.Web.Controllers
{
    public class NCGEquipmentItemController : CodexItemBaseController
    {
        //
        // GET: /NCGEquipmentItem/
        public ActionResult Index()
        {
            return View();
        }
	}



        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<NcgEquipmentItem> input)
        {

            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);

                //set item other properties if neccessary..
                storedItem.Gear = input.CodexItem.Gear;

                //..and save 
                DocumentSession.SaveChanges();
                Success("New equipment is created");
            }

            return View(input);
        }



}