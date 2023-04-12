using IT_Ticketing_System.Data;
using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace IT_Ticketing_System.Controllers
{
    public class AdminSignInController : Controller
    {
        private UserDbContext _db;
        public AdminSignInController(UserDbContext db)
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
        public IActionResult Register(AdminRegister obj)
        {
            //Check if passwords match
            if (obj.CompanyPassword != obj.RetypeCompanyPassword)
            {
                ModelState.AddModelError("RetypePassword", "Passwords do not match.");
            }

            //Check if email is already used in database
            var adminList = _db.Admins.ToList();
            foreach (Admin admin in adminList)
            {
                if (admin.CompanyName == obj.CompanyName)
                {
                    ModelState.AddModelError("Email", "A company account already exists with this name.");
                    return View(obj);
                }
            }

            //Extract User info from register data
            if (ModelState.IsValid)
            {
                Admin adminObj = new Admin();
                adminObj.CompanyName = obj.CompanyName;
                adminObj.CompanyPassword = obj.CompanyPassword;
                
                //Push to db
                _db.Admins.Add(adminObj);
                _db.SaveChanges();
                TempData["success"] = "Company account created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
