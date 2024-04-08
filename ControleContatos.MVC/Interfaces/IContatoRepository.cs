using ControleContatos.MVC.Models;

namespace ControleContatos.MVC.Interfaces
{
    public interface IContatoRepository
    {
        List<ContatoModel> BuscarTodos(int usuarioId);
        ContatoModel ListarPorId(int id);
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar (int id);
    }
}
