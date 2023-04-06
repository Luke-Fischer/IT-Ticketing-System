using IT_Ticketing_System.Data;
using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace IT_Ticketing_System.Controllers
{
    public class EmployeeInterfaceController : Controller
    {
        private UserDbContext _db;
        public EmployeeInterfaceController(UserDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(User obj)
        {
            IEnumerable<User> objCategoryList = _db.Users;
            foreach(User user in objCategoryList)
            {
                if(obj.Email == user.Email)
                {
					//get ID
					obj.Id = user.Id;
                }
            }
            IEnumerable<Ticket> ticketList = _db.Tickets.Where(p => p.UserId == obj.Id);
            return View(ticketList);

        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
    }
}
