using IT_Ticketing_System.Data;
using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IT_Ticketing_System.Controllers
{
    public class AdminInterfaceController : Controller
    {
        private static int? compId;
        private static string companyName;
        private static string companyUniqueId;

        private UserDbContext _db;
        public AdminInterfaceController(UserDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(Admin admin)
        {
            //Determine if account has added a connection string
            if(admin.CompanyName != null)
            {
                companyName = admin.CompanyName;
            }
            if(admin.AdminId != 0)
            {
                compId = admin.AdminId;
            }
            if(admin.CompanyUniqueIdentifer == "" || admin.CompanyUniqueIdentifer == null)
            {
                return View();
            }
            else
            {
                companyUniqueId = admin.CompanyUniqueIdentifer;

                var adminList = _db.Admins.ToList();

                foreach (Admin _admin in adminList)
                {
                    if (_admin.CompanyName == companyName)
                    {
                        compId = _admin.AdminId;

                        //Update entry with the unique identifier inputted
                        var updatingAdmin = _db.Admins.Find(compId);
                        if((updatingAdmin.CompanyUniqueIdentifer != "") && (updatingAdmin.CompanyUniqueIdentifer != null))
                        {
                            return RedirectToAction("Display");
                        }
                        updatingAdmin.CompanyUniqueIdentifer = companyUniqueId;
                        _db.Admins.Update(updatingAdmin);
                        _db.SaveChanges();
                        TempData["success"] = "Identifier string added successfully";
                        return RedirectToAction("Display");
                    }
                }
                return View();
            }
        }
        public IActionResult Display()
        {
            //Innit list
            List<Ticket> compList = new List<Ticket>();
            
            //Grab tickets from each user with the connecting id string
            var userList = _db.Users.ToList();
            foreach (User user in userList)
            {
                if(user.UserUniqueIdentfier == companyUniqueId)
                {
                    var ticketList = _db.Tickets.Where(p => p.UserId == user.Id);
                    foreach (Ticket ticket in ticketList)
                    {
                        compList.Add(ticket);
                    }
                }
            }
            IEnumerable<Ticket> fullList = compList;
            return View(fullList);
        }
    }
}
