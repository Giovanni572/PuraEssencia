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
           
            if (!ModelState.IsValid)
            {
                return View(usuario);
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

        public IActionResult EditarUsuario(int id)
        {
            var usuario = _usuarioRepositorio.ObterUsuarioPorId(id);
            if(usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarUsuario(int id, [Bind("id_usuario, nome, telefone, email")] Usuario usuario)
        {

            ModelState.Remove(nameof(usuario.senha));
            ModelState.Remove(nameof(usuario.confirmarsenha));

            if (id != usuario.id_usuario)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try {
                    if (_usuarioRepositorio.AtualizarUsuario(usuario))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                   ModelState.AddModelError("", "Erro ao atualizar usuário.");
                   return View(usuario);
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult ExcluirUsuario(int id)
        {
            _usuarioRepositorio.ExcluirUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
