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
        public ActionResult Index()
        {
            var model = _session.Query<AttributeInfo>().ToArray();
            return View(model);
        }

        public ActionResult Init()
        {
            _initDataService.InitData();
            ViewBag.Result = "Done";
            
            _session.SaveChanges();
            return View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Saved = false;
            var model = _session.Load<AttributeInfo>(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(AttributeInfo attribute)
        {
            var model = _session.Load<AttributeInfo>(attribute.Id);
            ViewBag.Saved = false;
            if (model != null)
            {
                model.Description = attribute.Description;
                _session.Store(model);
                _session.SaveChanges();
                ViewBag.Saved = true;
            }

            return View(model);
        }
    }
}