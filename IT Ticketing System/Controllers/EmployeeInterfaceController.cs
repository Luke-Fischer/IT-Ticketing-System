using IT_Ticketing_System.Data;
using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;
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
            return View(ticketList);
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
            Debug.WriteLine(obj.Title + obj.Message + obj.Priority + obj.timeSubmitted + obj.UserId);
			//Push to db
			_db.Tickets.Add(obj);
			_db.SaveChanges();
			TempData["success"] = "Ticket created successfully";
			return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int id)
        {
            /* if(id == null)
            {
                return NotFound();
            }*/
            var ticket = _db.Tickets.Find(id);

            if(ticket == null)
            {
                return NotFound();
            }
            ticketId = id;
            return View(ticket);
        }
        //UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}
