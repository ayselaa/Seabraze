using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Areas.Admin.Controllers
{
    [Area("admin")]
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
