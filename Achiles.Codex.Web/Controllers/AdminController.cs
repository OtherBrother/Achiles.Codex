using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Web.Misc;
using Achiles.Codex.Web.Models;
using Achiles.Codex.Web.Services;
using Microsoft.AspNet.Identity;

namespace Achiles.Codex.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : CodexItemController
    {
        private readonly IInitDataService _initDataService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IInitDataService initDataService, UserManager<ApplicationUser> userManager)
        {
            _initDataService = initDataService;
            _userManager = userManager;
        }
        
        public ActionResult Init()
        {
            _initDataService.InitData();
            DocumentSession.SaveChanges();

            Success("Success!", "Initialization done");

            return View();
        }

        public ActionResult Users()
        {
            return View(DocumentSession.Query<ApplicationUser>().ToList());
        }
        
        [HttpGet]
        public async Task<ActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            var model = new EditUserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                IsAdmin = user.Roles.Any(x => x.Role.Name == "Admin"),
                IsContributor = user.Roles.Any(x => x.Role.Name == "Contributor")
            };
            return View(model);
        }
        
        [HttpPost]
        public ActionResult EditUser(EditUserModel model)
        {
            var user = _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {

            }
            return View(model);
        }
    }
}