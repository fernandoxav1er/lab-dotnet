using FinShark.API.Data;
using FinShark.API.Dtos.Estoque;
using FinShark.API.Interfaces;
using FinShark.API.Mappers;
using FinShark.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.API.Repository
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly ApplicationDBContext _context;
        public EstoqueRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Estoque>> GetAllAsync()
        {
            return await _context.Estoques.Include(c => c.Comentarios).ToListAsync();
        }

        public async Task<Estoque?> GetByIdAsync(int id)
        {
            return await _context.Estoques.Include(c => c.Comentarios).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Estoque> CreateAsync(Estoque estoqueModel)
        {
            await _context.Estoques.AddAsync(estoqueModel);
            await _context.SaveChangesAsync();
            return estoqueModel;
        }   

        public async Task<Estoque?> UpdateAsync(int id, AtualizarEstoqueRequestDto estoqueDto)
        {
            var existeEstoque = await _context.Estoques.FirstOrDefaultAsync(x => x.Id == id);

            if (existeEstoque == null)
                return null;

            existeEstoque.Apelido = estoqueDto.Apelido;
            existeEstoque.NomeEmpresa = estoqueDto.NomeEmpresa;
            existeEstoque.ValorCompra = estoqueDto.ValorCompra;
            existeEstoque.Dividendo = estoqueDto.Dividendo;
            existeEstoque.Industria = estoqueDto.Industria;
            existeEstoque.Valuation = estoqueDto.Valuation;

            await _context.SaveChangesAsync(); 
            return existeEstoque;
        }

        public async Task<Estoque?> DeleteAsync(int id)
        {
            var estoqueModel = await _context.Estoques.FirstOrDefaultAsync(x => x.Id == id);

            if (estoqueModel == null)
            {
                return null;
            }

            _context.Estoques.Remove(estoqueModel);
            await _context.SaveChangesAsync();
            return estoqueModel;
        }

        public async Task<bool> EstoqueExist(int id)
        {
            return await _context.Estoques.AnyAsync(s => s.Id == id);
        }
    }
}
