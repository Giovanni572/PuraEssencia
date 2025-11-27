using Microsoft.AspNetCore.Mvc;
using ProjetoPuraEssencia.Models;
using ProjetoPuraEssencia.Repositorio;

namespace ProjetoPuraEssencia.Controllers
{


    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
      
        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            
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

            if (usuario != null && usuario.senha == senha)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Email e Senha Inválidos");
            return View();
        }


         [HttpGet]
         public IActionResult Cadastro()
         {
             return View();
         }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
           
            if (usuario.senha != usuario.confirmarsenha)
            {
                return RedirectToAction("Cadastro");
            }
            else {
                _usuarioRepositorio.RegistroUsuario(usuario);
                return RedirectToAction("Login");
            }
        }
    }
}
