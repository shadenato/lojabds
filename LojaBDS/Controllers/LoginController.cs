using Microsoft.AspNetCore.Mvc;
using LojaBDS.Models;
using LojaBDS.Repositorio;

namespace LojaBDS.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepositorio _loginRepositorio;

        public LoginController(LoginRepositorio loginRepositorio)
        {
            _loginRepositorio = loginRepositorio;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var login = _loginRepositorio.ObterUsuario(email);
            if (login != null && login.senhaUsuario == senha)
            {
                return RedirectToAction("Index", "Produto");
            }
            ModelState.AddModelError("", "Email ou senha inválidos.");

            return View();
        }
    }
}
