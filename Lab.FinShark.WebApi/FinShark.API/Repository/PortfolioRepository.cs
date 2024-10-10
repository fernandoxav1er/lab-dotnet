using FinShark.API.Data;
using FinShark.API.Interfaces;
using FinShark.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.API.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;    
        }

        public async Task<List<Estoque>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios
                .Where(u => u.AppUserId == user.Id)
                .Select(estoque => new Estoque
                {
                    Id = estoque.EstoqueId,
                    Apelido = estoque.Estoque.Apelido,
                    NomeEmpresa = estoque.Estoque.NomeEmpresa,
                    ValorCompra = estoque.Estoque.ValorCompra,
                    Dividendo = estoque.Estoque.Dividendo,
                    Industria = estoque.Estoque.Industria,
                    Valuation = estoque.Estoque.Valuation
                }).ToListAsync();
        }

        public async Task<Portfolio> Create(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
    }
}
