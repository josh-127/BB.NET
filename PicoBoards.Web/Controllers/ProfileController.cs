using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PicoBoards.Web.Controllers
{
    public class ProfileController : Controller
    {
        public async Task<IActionResult> Index(string userName)
        {
            return View();
        }
    }
}