using FinShark.API.Models;

namespace FinShark.API.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Estoque>> GetUserPortfolio(AppUser user);
        Task<Portfolio> Create(Portfolio portfolio);
    }
}
