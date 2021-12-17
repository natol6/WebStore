using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
