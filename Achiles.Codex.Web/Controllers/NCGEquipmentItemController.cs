﻿using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class NCGEquipmentItemController : CodexItemBaseController
    {
        //
        // GET: /NCGEquipmentItem/
        public ActionResult Index()
        {
          
            return View();
        }

        public ActionResult Edit(string id)
        {
            var model = GetModel<NcgEquipmentItem>(id);
            return View(model);
        }
       

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(CodexItemModel<NcgEquipmentItem> input)
        {
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);
                //..and save 
                DocumentSession.SaveChanges();
                Success("New equipment item is created");
            }
            return View(input);
        }


    }
}