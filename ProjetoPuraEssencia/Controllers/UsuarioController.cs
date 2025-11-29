using Microsoft.AspNetCore.Mvc;
using ProjetoPuraEssencia.Repositorio;

namespace ProjetoPuraEssencia.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;//declaracao

        //construtor
        public UsuarioController(UsuarioRepositorio usuarioRepositorio)

        {//instancia
            _usuarioRepositorio = usuarioRepositorio;
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]


        public IActionResult Login(string email, string senha)
        {
            var usuario = _usuarioRepositorio.ObterUsuario(email);
            if (usuario != null && usuario.Senha == senha)
            {
                // Lógica de autenticação bem-sucedida
                return RedirectToAction("ListarProduto", "Produto");
            }
            // Lógica de autenticação falhou
            ModelState.AddModelError("", "Email ou senha Inválidos.");
            
            return View();
        }
    }
}
