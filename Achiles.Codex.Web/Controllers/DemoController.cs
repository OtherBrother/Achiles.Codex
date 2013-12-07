using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
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

       

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Rule(CodexItemModel<Rule> input)
        {

            if (ModelState.IsValid)
            {
                //insert or update properties common for all base codex items
                var storedItem = UpsertBaseCodexItem(input);

                //set item other properties if neccessary..
                storedItem.Gear = input.CodexItem.Gear;

                //..and save 
                DocumentSession.SaveChanges();
                Success("Nice work!","Rules are made to be broken!");
            }

            return View(input);
        }
    }
}