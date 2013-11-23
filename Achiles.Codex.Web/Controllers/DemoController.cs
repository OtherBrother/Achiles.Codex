using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Achiles.Codex.Web.Controllers
{
    public class DemoController : Controller
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
	}
}