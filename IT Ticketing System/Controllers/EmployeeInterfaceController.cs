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
        public EmployeeInterfaceController(UserDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(User obj)
        {
            Debug.WriteLine(obj.Email + "\n\n\n\n");

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
            Debug.WriteLine(obj.Title + obj.Message + obj.Priority + obj.timeSubmitted + obj.UserId);
			//Push to db
			_db.Tickets.Add(obj);
			_db.SaveChanges();
			TempData["success"] = "Ticket created successfully";
			return RedirectToAction("Index");
        }
    }
}
