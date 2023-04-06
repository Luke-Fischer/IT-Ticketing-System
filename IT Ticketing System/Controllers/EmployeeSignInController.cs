using IT_Ticketing_System.Data;
using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IT_Ticketing_System.Controllers
{
    public class EmployeeSignInController : Controller
    {
        private UserDbContext _db;
        public EmployeeSignInController(UserDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register obj)
        {
            if(obj.Password != obj.retypePassword)
            {
                ModelState.AddModelError("RetypePassword", "Passwords do not match.");
            }
            //Extract User info from register data
            if (ModelState.IsValid)
            {
                User userObj = new User();
                userObj.Email = obj.Email;
                userObj.Password = obj.Password;

                //Push to db
                _db.Users.Add(userObj);
                _db.SaveChanges();
                TempData["success"] = "Account created successfully";
                return RedirectToAction("Index");
            }
            
            return View(obj);
        }
    }
}
