﻿using IT_Ticketing_System.Data;
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

        public IActionResult Index(User obj)
        {
            var userList = _db.Users.ToList();
            foreach (User user in userList)
            {
                if ((user.Email == obj.Email) && (user.Password == obj.Password))
                {
                    return RedirectToAction("Index", "EmployeeInterface");
                }
            }
            if(!(obj.Email == null && obj.Password == null))
            {
                ModelState.AddModelError("Password", "Incorrect Email or Password.");
            }
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
            //Check if passwords match
            if(obj.Password != obj.retypePassword)
            {
                ModelState.AddModelError("RetypePassword", "Passwords do not match.");
            }

            //Check if email is already used in database
            var userList = _db.Users.ToList();
            foreach(User user in userList)
            {
                if(user.Email == obj.Email)
                {
                    ModelState.AddModelError("Email", "Account already exists with this email address.");
                    return View(obj);
                }
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
