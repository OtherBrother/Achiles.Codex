using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Achilles.Codex.Web.Misc;
using Achilles.Codex.Web.Models;
using Achilles.Codex.Web.Services;
using Microsoft.AspNet.Identity;

namespace Achilles.Codex.Web.Controllers
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
        public async Task<ActionResult> EditUser(EditUserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                if (model.IsAdmin)
                {
                    var result =await _userManager.AddToRoleAsync(user.Id, "Admin");
                    
                    if (result.Succeeded)
                        Success("You asked for it.","Another admin..");
                }
                else
                {
                    var result = await _userManager.RemoveFromRoleAsync(user.Id, "Admin");
                    if(result.Succeeded)
                        Success("Thats the spirit. Less admins less hassle");

                }
                if (model.IsContributor)
                { 
                    var result  =  await _userManager.AddToRoleAsync(user.Id, "Contributor");
                    if(result.Succeeded)
                        Success("Congrats!","More contributors - more content, but can the same be said about quality?");
                    
                 }
                else
                {
                    var result = await _userManager.RemoveFromRoleAsync(user.Id, "Contributor");
                    if (result.Succeeded)
                    {
                        Success("Great success!", "Wasn't contributing much anyways..");
                    }
                }

            }
            return View(model);
        }
    }
}