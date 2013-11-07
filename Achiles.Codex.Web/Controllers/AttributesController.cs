using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Web.Models;
using Achiles.Codex.Web.Services;
using Raven.Client;

namespace Achiles.Codex.Web.Controllers
{
    public class AttributesController : Controller
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
            var model = _session.Query<AttributeInfo>().ToArray().OrderBy(a=>a.Order);
            
            if (deleted)
                ViewBag.DeletedMessage = "Oh well, attribute is gone now..";

            return View("Index",model);
        }

        public ActionResult Init()
        {
            _initDataService.InitData();
            ViewBag.Result = "Done";
            
            _session.SaveChanges();
            return View();
        }
        
        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.Saved = false;
            
            var model = _session.Load<AttributeInfo>(id);
            @ViewBag.Title = string.Format("Edit {0}", model.Name);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Edit(AttributeInfo model)
        {
            
            ViewBag.Saved = false;

            var entity = _session.Load<AttributeInfo>(model.Id);
            if (entity != null)
            {
                @ViewBag.Title = string.Format("Edit {0}", entity.Name);
                entity.Description = model.Description;
                entity.Order = model.Order;
                _session.SaveChanges();
                ViewBag.Saved = true;
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