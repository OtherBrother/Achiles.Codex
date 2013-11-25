using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Models;

namespace Achiles.Codex.Web.Controllers
{
    public class DemoController : CodexItemBaseController
    {
        //
        // GET: /Demo/
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult NewArticle()
        {
            return View();
        }
        public ActionResult NewMeleeWeapon()
        {
            return View();
        }
        public ActionResult NewMeleeWeaponJonas()
        {
            return View();
        }

        public ActionResult Rule(string id)
        {
            var model = GetModel<Rule>(id);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Rule(Rule input)
        {
            Rule storedItem = null;
            
            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                storedItem = UpsertBaseCodexItem(input);
                
                //set item other properties if neccessary..
                storedItem.Gear = input.Gear;

                //..and save 
                DocumentSession.SaveChanges();
            }
            
            return View(CreateModel(storedItem ?? input));
        }

	}
}