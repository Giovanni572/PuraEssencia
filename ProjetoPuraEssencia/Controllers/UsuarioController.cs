using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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
                if (usuario.tipo == "Administrador")
                {
                    return RedirectToAction("Index", "Usuario");
                }
                else if(usuario.tipo == "Cliente")
                {
                    return RedirectToAction("Index", "Home");
                }
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

        [HttpGet]
        public IActionResult Index()
        {
            return View(_usuarioRepositorio.TodosUsuarios());
        }

        public IActionResult ExcluirUsuario(int id)
        {
            _usuarioRepositorio.ExcluirUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
