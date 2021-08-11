using Health.Web.App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerBI.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health.Web.App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleControllercs : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

      
        public RoleControllercs(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

        }


        /// <summary>
        /// para get all roles
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var Roles = _roleManager.Roles.ToList();


            return View(Roles);
        }

        public IActionResult Create()
        {

            return View(new IdentityRole());
        }


        /// <summary>
        /// for create a roles
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(IdentityRole roles)
        {
            await _roleManager.CreateAsync(roles);

          
                return RedirectToAction("Index");

        }


    }
   
   
}
