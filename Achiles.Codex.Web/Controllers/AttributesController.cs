﻿using System.Linq;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Services;
using Raven.Client;

namespace Achilles.Codex.Web.Controllers
{
    public class AttributesController : CodexItemController
    {
        public ActionResult Index(bool deleted = false)
        {
            ViewBag.Title = "Attributes";
            var model = DocumentSession.Query<AttributeInfo>().ToArray().OrderBy(a=>a.Order);

            if (deleted)
                Success("Oh well..","Attribute is gone now..");

            return View(model);
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
        [ValidateInput(false)]
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
