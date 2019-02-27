using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;

using REERP.Models;
using REERP.Security;
using REERP.Product.Services;

namespace REERP.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<MyIdentityUser> userManager;
        private RoleManager<MyIdentityRole> roleManager;
        private readonly IBranchService _branchService;

        public AccountController(IBranchService branchService)
        {
            _branchService = branchService;

            MyIdentityDbContext db = new MyIdentityDbContext();

            UserStore<MyIdentityUser> userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);

            RoleStore<MyIdentityRole> roleStore = new RoleStore<MyIdentityRole>(db);
            roleManager = new RoleManager<MyIdentityRole>(roleStore);

        }
        
        public ActionResult Register()
        {
            ViewBag.RoleList = new SelectList(roleManager.Roles, "Id", "Name");
            ViewBag.BranchList = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model, string RoleList, int BranchList)
        {
            ViewBag.RoleList = new SelectList(roleManager.Roles, "Id", "Name");
            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.BirthDate = model.BirthDate;
                user.Bio = model.Bio;
                user.Role = roleManager.FindById(RoleList).Name;
                user.BranchId = BranchList;

                IdentityResult result = userManager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, roleManager.FindById(RoleList).Name);
                    return RedirectToAction("Users", "Account");
                }
                else
                {
                    ModelState.AddModelError("UserName", "Error while creating the user!");
                }
            }
            return View(model);
        }

        public ActionResult EditUser(string id)
        {
            ViewBag.RoleList = new SelectList(roleManager.Roles, "Id", "Name");
            ViewBag.BranchList = new SelectList(_branchService.GetAllBranches(), "BranchId", "BranchName");
            var user = userManager.FindById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(Register model, string RoleList, int BranchList)
        {
            var user = userManager.FindById(model.Id);
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FullName = model.FullName;
            user.BirthDate = model.BirthDate;
            user.Bio = model.Bio;
            user.BranchId = BranchList;

            string userRole = null;
            if (user.Role != roleManager.FindById(RoleList).Name)
            {
                userRole = user.Role;
                user.Role = roleManager.FindById(RoleList).Name;                
            }
            
            IdentityResult result = userManager.Update(user);

            if (result.Succeeded)
            {
                if (userRole!=null)
                {
                    userManager.RemoveFromRole(user.Id, userRole);
                    userManager.AddToRole(user.Id, user.Role);
                }               
                return RedirectToAction("Users", "Account");
            }
            else
            {
                ModelState.AddModelError("UserName", "Error while editing the user!");
            }
            return RedirectToAction("EditUser", "Account", new { id = user.Id });
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                MyIdentityUser user = userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    Session["USER_INFO"] = user;
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }


        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);
                IdentityResult result = userManager.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Error while changing the password.");
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult ChangeProfile()
        {
            MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);
            ChangeProfile model = new ChangeProfile();
            model.FullName = user.FullName;
            model.BirthDate = user.BirthDate;
            model.Bio = user.Bio;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile(ChangeProfile model)
        {
            if (ModelState.IsValid)
            {
                MyIdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);
                user.FullName = model.FullName;
                user.BirthDate = model.BirthDate;
                user.Bio = model.Bio;
                IdentityResult result = userManager.Update(user);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Profile updated successfully.";
                }
                else
                {
                    ModelState.AddModelError("", "Error while saving profile.");
                }
            }
            return View(model);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public ActionResult Users()
        {
            var users = userManager.Users;
            var registers = new List<Register>();
            foreach(var user in users)
            {
                var register = new Register
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    BirthDate = user.BirthDate,
                    Bio = user.Bio,
                    BranchId = user.BranchId,
                    BranchName = _branchService.FindById(user.BranchId).BranchName,
                    Role = user.Role
                };
                registers.Add(register);
            }
            return View(registers);
        }

        [Authorize]
        public ActionResult DeleteUser(string id)
        {
            var user = userManager.FindById(id);
            userManager.Delete(user);
            return RedirectToAction("Users", "Account");
        }
    }
}