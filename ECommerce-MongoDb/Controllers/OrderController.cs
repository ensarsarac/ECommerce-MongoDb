using Microsoft.AspNetCore.Mvc;

namespace ECommerce_MongoDb.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
