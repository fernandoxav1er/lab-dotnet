using IntegraBrasil.API.Dtos;
using IntegraBrasil.API.Models;

namespace IntegraBrasil.API.Interfaces
{
    public interface IBrasilApi
    {
        Task<ResponseGenerico<Endereco>> BuscarEnderecoPorCEP(string cep);
        Task<ResponseGenerico<List<Banco>>> BuscarTodosBancos();
        Task<ResponseGenerico<Banco>> BuscarBanco(string codigoBanco);
    }
}
