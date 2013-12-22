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
	}
}