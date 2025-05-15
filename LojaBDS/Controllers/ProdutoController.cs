using Microsoft.AspNetCore.Mvc;
using LojaBDS.Models;
using LojaBDS.Repositorio;

namespace LojaBDS.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
