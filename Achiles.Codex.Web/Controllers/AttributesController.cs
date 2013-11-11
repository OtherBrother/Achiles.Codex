using System.Linq;
using System.Web.Mvc;
using Achiles.Codex.Web.Models;
using Achiles.Codex.Web.Services;
using Raven.Client;

namespace Achiles.Codex.Web.Controllers
{
    public class AttributesController : BaseController
    {
        private readonly IDocumentSession _session;
        private readonly IInitDataService _initDataService;

        public AttributesController(IDocumentSession session, IInitDataService initDataService)
        {
            _session = session;
            _initDataService = initDataService;
        }

        // GET: /Attributes/
        public ActionResult Index(bool deleted = false)
        {
            ViewBag.Title = "Attributes";
            var model = _session.Query<AttributeInfo>().ToArray().OrderBy(a=>a.Order);

            if (deleted)
                Success("Oh well..","Attribute is gone now..");

            return View(model);
        }

        public ActionResult Init()
        {
            _initDataService.InitData();
            _session.SaveChanges();
            
            Success("Success!", "Initialization done");
            
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(string id)
        {
            var model = _session.Load<AttributeInfo>(id);
            @ViewBag.Title = string.Format("Edit {0}", model.Name);
            return View(model);
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AttributeInfo model)
        {
            var entity = _session.Load<AttributeInfo>(model.Id);
            if (entity != null)
            {
                @ViewBag.Title = string.Format("Edit {0}", entity.Name);
                entity.Description = model.Description;
                entity.Order = model.Order;
                _session.SaveChanges();

                Success("Hooray!", "Now it is much better.");
            }

            return View(entity);
        }

        public ActionResult Delete(string id)
        {
            var entity = _session.Load<AttributeInfo>(id);
            if (entity != null)
            {
                _session.Delete(entity);
                _session.SaveChanges();
                
            }
            return RedirectToAction("Index", new { Deleted = true});
        }
    }
}