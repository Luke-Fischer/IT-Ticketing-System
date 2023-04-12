using IT_Ticketing_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace IT_Ticketing_System.Controllers
{
    public class AdminInterfaceController : Controller
    {
        public IActionResult Index(Admin admin)
        {
            return View();
        }
    }
}
