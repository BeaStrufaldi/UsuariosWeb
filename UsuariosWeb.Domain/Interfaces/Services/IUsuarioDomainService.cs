using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;

namespace UsuariosWeb.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        void CadastrarUsuario(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
        void ExcluirUsuario(Usuario usuario);

        Usuario AutenticarUsuario(string email, string senha);

        Usuario ObterUsuario(string email);
        Usuario ObterUsuario(Guid idUsuario);

        List<Usuario> ConsultarUsuarios();

        List<Perfil> ConsultarPerfis();
        Perfil ObterPerfil(Guid idPerfil);
    }
}
