using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProjectDay3.Models;
using MVCProjectDay3.ViewModels.Account;

namespace MVCProjectDay3.Controllers
{
    public class AccountController(UserManager<User> _userManager,SignInManager<User>_signInManager,RoleManager<Role>_roleManager) : Controller
    {
        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if(!ModelState.IsValid) 
            return View();
            User user = new()
            {
                FullName = vm.FullName,
                UserName = vm.Username,
                Email = vm.Email
            };
            var result = await _userManager.CreateAsync(user, vm.Password);
                if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
                var roleResult=await _userManager.AddToRoleAsync(user,RoleStoreBase.User)
        }
    }
}
