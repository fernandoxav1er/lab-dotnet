using ControleContatos.MVC.Models;

namespace ControleContatos.MVC.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario (UsuarioModel usuario);
        void RemoverSessaoUsuario();
        UsuarioModel GetSessaoUsuario();
    }
}
