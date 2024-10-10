using ControleContatos.MVC.Models;

namespace ControleContatos.MVC.Interfaces
{
    public interface IUsuarioRepository
    {
        List<UsuarioModel> BuscarTodos();
        UsuarioModel ListarPorId(int id);
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email, string login);
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioModel usuario);
        bool Apagar (int id);
        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha);
    }
}
