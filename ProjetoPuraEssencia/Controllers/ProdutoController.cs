using Microsoft.AspNetCore.Mvc;

namespace ProjetoPuraEssencia.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult ListarProduto()
        {
            return View();
        }
    }
}
