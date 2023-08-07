using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;

namespace Sales_Report.Controllers
{
    
    
    
    [Authorize]
    public class AppRolesController : Controller
    { 

        private readonly RoleManager<IdentityRole> _roleManager;
       
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
                _roleManager = roleManager;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var role = _roleManager.Roles;
            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> Create() 
        {
           return View();
        }

        // Save eDate into Identity -> Role Manaager
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (! _roleManager.RoleExistsAsync(model.Name.ToLower()).GetAwaiter().GetResult()) 
            
            {
             _roleManager.CreateAsync(new IdentityRole(model.Name.ToLower())).GetAwaiter().GetResult();  
            }
            return RedirectToAction("Index");
        }
    }
}
