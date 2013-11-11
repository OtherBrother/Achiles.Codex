using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Web.Models;
using Achiles.Codex.Web.Services;
using Microsoft.AspNet.Identity;

namespace Achiles.Codex.Web.Controllers
{
    public class AdminController : CodexItemController
    {
        private readonly IInitDataService _initDataService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IInitDataService initDataService, UserManager<ApplicationUser> userManager)
        {
            _initDataService = initDataService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Init()
        {
            _initDataService.InitData();
            DocumentSession.SaveChanges();

            Success("Success!", "Initialization done");

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {
            return View(DocumentSession.Query<ApplicationUser>().ToList());
        }

	}
}