using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Repositories;
using UsuariosWeb.Domain.Interfaces.Services;

namespace UsuariosWeb.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            var item = _usuarioRepository.Obter(usuario.Email);

            if(item != null && item.IdUsuario != item.IdUsuario)
            {
                throw new Exception($"{usuario.Email} Já cadastrado");
            }

            _usuarioRepository.Alterar(usuario);
        }

        public Usuario AutenticarUsuario(string email, string senha)
        {
            Usuario usuario = _usuarioRepository.Obter(email, senha);
            if(usuario != null)
            {
                return usuario;
            }else{
                throw new Exception("Acesso negado");
            }
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            if(_usuarioRepository.Obter(usuario.Email) != null)
            {
                throw new Exception(usuario.Email);
            }

            var perfil = _perfilRepository.Obter("default");
            usuario.IdPerfil = perfil.IdPerfil;

            _usuarioRepository.Inserir(usuario);
        }

        public List<Perfil> ConsultarPerfis()
        {
            return _perfilRepository.Consultar();
        }

        public List<Usuario> ConsultarUsuarios()
        {
            return _usuarioRepository.Consultar();
        }

        public void ExcluirUsuario(Usuario usuario)
        {
            _usuarioRepository.Excluir(usuario);
        }

        public Perfil ObterPerfil(Guid idPerfil)
        {
            return _perfilRepository.ObterPorId(idPerfil);
        }

        public Usuario ObterUsuario(string email)
        {
            var usuario = _usuarioRepository.Obter(email);

            if(usuario != null)
            {
                usuario.Perfil = _perfilRepository.ObterPorId(usuario.IdPerfil);

                return usuario;
            }
            else
            {
                throw new Exception("Usuario não encontrado");
            }
        }
        public Usuario ObterUsuario(Guid idUsuario)
        {
            return _usuarioRepository.ObterPorId(idUsuario);
        }

            
    }
}
