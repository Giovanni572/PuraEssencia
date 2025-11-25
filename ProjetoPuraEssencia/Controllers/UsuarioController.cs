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
            if (usuario != null && usuario.senha == senha)
            {
                // Lógica de autenticação bem-sucedida
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Email ou senha Inválidos.");
            return View();
            {
                // Lógica de autenticação falhou
                ViewBag.Erro = "Email ou senha inválidos.";
                return View();
            }

        }
}
