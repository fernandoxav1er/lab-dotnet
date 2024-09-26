using ControleContatos.MVC.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.MVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
