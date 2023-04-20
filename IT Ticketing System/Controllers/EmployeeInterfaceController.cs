using IT_Ticketing_System.Data;
using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Completion;
using System.Diagnostics;

namespace IT_Ticketing_System.Controllers
{
    public class EmployeeInterfaceController : Controller
    {
        private UserDbContext _db;
        private static int _userId;
        private static int ticketId;
        public EmployeeInterfaceController(UserDbContext db)
        {
            _db = db;
        }
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Index(User obj)
        {
            var userList = _db.Users.ToList();
            foreach (User tempUser in userList)
            {
                if (tempUser.Email == obj.Email)
                {
                    _userId = tempUser.Id;
                }
            }
            IEnumerable<Ticket> ticketList = _db.Tickets.Where(p => p.UserId == _userId);

            if(ticketList.Count() == 0)
            {
                return RedirectToAction("Empty");
            }
            return View(ticketList);
        }

        public IActionResult Empty()
        {
            return View();
        }
        //GET
        public IActionResult Create()
		{
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Ticket obj)
        {
            obj.Status = "pending";
            obj.UserId = _userId;
            obj.timeSubmitted = DateTime.Now;
            obj.UserEmail = _db.Users.Find(_userId).Email;
            Debug.WriteLine(obj.Title + obj.Message + obj.Priority + obj.timeSubmitted + obj.UserId);
			//Push to db
			_db.Tickets.Add(obj);
			_db.SaveChanges();
			TempData["success"] = "Ticket created successfully";
			return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int? id)
        {
             if(id == null)
            {
                return NotFound();
            }
            var ticket = _db.Tickets.Find(id);

            if(ticket == null)
            {
                return NotFound();
            }
            ticketId = (int)id;
            
            return View(ticket);
        }
        //UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Edit(Ticket obj)
        {
            var updatingTicket = _db.Tickets.Find(ticketId);
            updatingTicket.Title = obj.Title;
            updatingTicket.Priority = obj.Priority;
            if(obj.Message != null)
            {
                updatingTicket.Message = obj.Message;
            }
            updatingTicket.timeSubmitted = DateTime.Now;
            _db.Tickets.Update(updatingTicket);
            _db.SaveChanges();
            TempData["success"] = "Ticket updated successfully";
            return RedirectToAction("Index");
        }

        //DELETE
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Delete(int? id)
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
            _db.Tickets.Remove(ticket);
            _db.SaveChanges();
            TempData["success"] = "Ticket deleted successfully";
            return RedirectToAction("Index");
        }
        //Retrieve users company string
        public IActionResult Connect()
        {
            return View();
        }
        //UPDATE
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult ConnectPOST(User obj)
        {
            //See if this is a valid key
            var adminList = _db.Admins.ToList();
            foreach(Admin admin in adminList)
            {
                if(admin.CompanyUniqueIdentifer == obj.UserUniqueIdentfier)
                {
                    User user = _db.Users.Find(_userId);
                    user.UserUniqueIdentfier = obj.UserUniqueIdentfier;
                    _db.Update(user);
                    _db.SaveChanges();
                    TempData["success"] = "Connection to your company made successfully";
                    return RedirectToAction("Index");   
                }
            }
            ViewBag.ErrorMessage = "No company exists with this key. Please contact your administrator for the correct key.";
            return View(obj);
        }
        //Retrieve users company string
        public IActionResult ViewConnection()
        {
            InfoConnection info = new InfoConnection();
            User usr = _db.Users.Find(_userId);
            info.key = usr.UserUniqueIdentfier.ToString();
            var compList = _db.Admins.ToList();
            foreach(Admin ad in compList)
            {
                if(ad.CompanyUniqueIdentifer == usr.UserUniqueIdentfier)
                {
                    info.CompName = ad.CompanyName;
                    break;
                }
            }
            return View(info);
        }
    }
}
