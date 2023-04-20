using IT_Ticketing_System.Data;
using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IT_Ticketing_System.Controllers
{
    public class AdminSignInController : Controller
    {
        private UserDbContext _db;
        public AdminSignInController(UserDbContext db)
        {
            _db = db;
        }
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Index(Admin obj)
        {
            Md5Security security = new Md5Security();
            var adminList = _db.Admins.ToList();
            foreach (Admin admin in adminList)
            {
                if ((admin.CompanyName == obj.CompanyName) && (admin.CompanyPassword == security.Encrypt(obj.CompanyPassword)))
                {
                    return RedirectToAction("Index", "AdminInterface", admin);
                }
            }
            if (!(obj.CompanyName == null && obj.CompanyPassword == null))
            {
                ViewBag.ErrorMessage = "Incorrect Company Name or Password";
            }
            return View();
        }
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Register()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Register(AdminRegister obj)
        {
            //Check if passwords match
            if (obj.CompanyPassword != obj.RetypeCompanyPassword)
            {
                ModelState.AddModelError("RetypeCompanyPassword", "Passwords do not match.");
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
                Md5Security security = new Md5Security();
                adminObj.CompanyPassword = security.Encrypt(obj.CompanyPassword);
                adminObj.CompanyUniqueIdentifer = "";
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
