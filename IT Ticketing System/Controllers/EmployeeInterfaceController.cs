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
            return View();
        }
    }
}
