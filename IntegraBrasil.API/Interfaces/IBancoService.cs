using IntegraBrasil.API.Dtos;
using IntegraBrasil.API.Models;

namespace IntegraBrasil.API.Interfaces
{
    public interface IBancoService
    {
        Task<ResponseGenerico<List<BancoResponse>>> BuscarTodosBancos();
        Task<ResponseGenerico<BancoResponse>> BuscarBanco(string codigoBanco);

    }
}
