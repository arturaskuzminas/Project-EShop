﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if ((user == null))
            {
                ViewBag.ErrorMessage = $"Naudotojas su Id = {id} nebuvo rastas";
                return View("NotFound");
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                CityID = user.CityID,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = (List<string>)userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Naudotojas su Id = {model.Id} nebuvo rastas";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.CityID = model.CityID;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Naudotojas su Id = {userId} nebuvo rastas";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Naudotojas su Id = {userId} nebuvo rastas";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Negali pašalinti naudotojo iš egzistuojančių rolių");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Negalima pridėti pasirinktų rolių naudotojui");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Naudotojas su Id = {id} nebuvo rastas";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }
    }
}
