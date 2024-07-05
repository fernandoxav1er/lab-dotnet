using FinShark.API.Data;
using FinShark.API.Dtos.Estoque;
using FinShark.API.Helpers;
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

        public async Task<List<Estoque>> GetAll(QueryObject query)
        {
            var estoques = _context.Estoques.Include(c => c.Comentarios).AsQueryable();
            
            if (string.IsNullOrWhiteSpace(query.CompanyName) == false)
                estoques = estoques.Where(s => s.NomeEmpresa.Contains(query.CompanyName));

            if (string.IsNullOrWhiteSpace(query.Symbol) == false)
                estoques = estoques.Where(s => s.Apelido.Contains(query.Symbol));

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    estoques = query.IsDescending ? estoques.OrderByDescending(s => s.Apelido) : estoques.OrderBy(s => s.Apelido);
                }
            }

            return await estoques.ToListAsync();

        }
        public async Task<List<Estoque>> GetPagination(QueryPagination query)
        {
            var estoques = _context.Estoques.Include(c => c.Comentarios).AsQueryable();

            if (string.IsNullOrWhiteSpace(query.CompanyName) == false)
                estoques = estoques.Where(s => s.NomeEmpresa.Contains(query.CompanyName));

            if (string.IsNullOrWhiteSpace(query.Symbol) == false)
                estoques = estoques.Where(s => s.Apelido.Contains(query.Symbol));

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    estoques = query.IsDescending ? estoques.OrderByDescending(s => s.Apelido) : estoques.OrderBy(s => s.Apelido);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await estoques.Skip(skipNumber).Take(query.PageSize).ToListAsync(); 

        }

        public async Task<Estoque?> GetById(int id)
        {
            return await _context.Estoques.Include(c => c.Comentarios).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Estoque> Create(Estoque estoqueModel)
        {
            await _context.Estoques.AddAsync(estoqueModel);
            await _context.SaveChangesAsync();
            return estoqueModel;
        }   

        public async Task<Estoque?> Update(int id, AtualizarEstoqueRequestDto estoqueDto)
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

        public async Task<Estoque?> Delete(int id)
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

        public async Task<Estoque?> GetBySymbol(string symbol)
        {
            return await _context.Estoques.FirstOrDefaultAsync(s => s.Apelido == symbol);
        }
    }
}
