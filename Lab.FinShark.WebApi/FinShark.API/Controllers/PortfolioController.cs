using FinShark.API.Extensions;
using FinShark.API.Interfaces;
using FinShark.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.API.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEstoqueRepository _estoqueRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(UserManager<AppUser> userManager, 
            IEstoqueRepository estoqueRepo,
            IPortfolioRepository portfolioRepo) 
        {
            _userManager = userManager;
            _estoqueRepo = estoqueRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var estoque = await _estoqueRepo.GetBySymbol(symbol);

            if (estoque == null) return BadRequest("Not found!");

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            if (userPortfolio.Any(e => e.Apelido.ToLower() == symbol.ToLower())) 
                return BadRequest("Não é possivel adicioanr o mesmo estoque ao portifolio");

            var portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                EstoqueId = estoque.Id,
            };

            await _portfolioRepo.Create(portfolioModel);

            if (portfolioModel == null) return StatusCode(500, "Erro ao criar!");

            return Created();

        }

    }
}
