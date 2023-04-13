using IT_Ticketing_System.Data;
using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using System.Diagnostics;
using System.Drawing.Text;

namespace IT_Ticketing_System.Controllers
{
    public class AdminInterfaceController : Controller
    {
        private static int? compId;
        private static string companyName;
        private static string companyUniqueId;
        private static int ticketId;

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
                        //Check if this key is already in use
                        var coList = _db.Admins.ToList();
                        if(coList != null)
                        {
                            foreach (Admin co in coList)
                            {
                                if (co.CompanyUniqueIdentifer == companyUniqueId)
                                {
                                    ViewBag.ErrorMessage = "This key is already in use.  ";
                                    return View();
                                }
                            }
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
        //GET
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = _db.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound();
            }
            ticketId = (int)id;

            return View(ticket);
        }
        //UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Ticket obj)
        {
            var updatingTicket = _db.Tickets.Find(ticketId);
            updatingTicket.Status = obj.Status;
            updatingTicket.timeSubmitted = DateTime.Now;
            _db.Tickets.Update(updatingTicket);
            _db.SaveChanges();
            TempData["success"] = "Ticket updated successfully";
            return RedirectToAction("Display");
        }
    }
}
