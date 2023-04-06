using Microsoft.AspNetCore.Mvc;

namespace IT_Ticketing_System.Controllers
{
    public class EmployeeInterfaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
