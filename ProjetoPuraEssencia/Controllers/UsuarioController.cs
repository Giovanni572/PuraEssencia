using Microsoft.AspNetCore.Mvc;

namespace ProjetoPuraEssencia.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
