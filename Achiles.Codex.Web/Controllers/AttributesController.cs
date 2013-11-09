using System.Linq;
using System.Web.Mvc;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Services;
using Raven.Client;

namespace Achiles.Codex.Web.Controllers
{
    public class AttributesController : CodexItemController
    {

        private readonly IInitDataService _initDataService;

        public AttributesController(IInitDataService initDataService)
        {
            _initDataService = initDataService;
        }

        public ActionResult Index(bool deleted = false)
        {
            ViewBag.Title = "Attributes";
            var model = DocumentSession.Query<AttributeInfo>().ToArray().OrderBy(a=>a.Order);

            if (deleted)
                Success("Oh well..","Attribute is gone now..");

            return View(model);
        }
         
        [Authorize(Roles = "Admin")]
        public ActionResult Init()
        {
            _initDataService.InitData();
            DocumentSession.SaveChanges();
            
            Success("Success!", "Initialization done");
            
            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(string id)
        {
            var model = DocumentSession.Load<AttributeInfo>(id);
            @ViewBag.Title = string.Format("Edit {0}", model.Name);
            return View(model);
        }
        
        [HttpPost]
        [Authorize(Roles = "Contributor")]
        public ActionResult Edit(AttributeInfo input)
        {
            var entity = DocumentSession.Load<AttributeInfo>(input.Id);
            @ViewBag.Title = string.Format("Edit {0}", entity.Name);

            UpdateProperties(entity, input);           
            DocumentSession.SaveChanges();

            Success("Hooray!", "Now it is much better.");

            return View(entity);
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
