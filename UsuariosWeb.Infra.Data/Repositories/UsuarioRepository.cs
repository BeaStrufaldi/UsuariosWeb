using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosWeb.Domain.Entities;
using UsuariosWeb.Domain.Interfaces.Repositories;

namespace UsuariosWeb.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string _connecctionString;

        public UsuarioRepository(string connecctionString)
        {
            _connecctionString = connecctionString;
        }
        public void Alterar(Usuario entity)
        {
            var query = @"
                UPDATE USUARIO SET
                            NOME = @Nome, EMAIL = @Email, IDPERFIL = @IdPerfil
                WHERE 
                            IDUSUARIO = @IdUsuario";

            using (var connection = new SqlConnection(_connecctionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Usuario> Consultar()
        {
            var query = @"
                SELECT * FROM USUARIO
                ORDER BY NOME";

            using (var connection = new SqlConnection(_connecctionString))
            {
                return connection.Query<Usuario>(query).ToList();
            }
        }

        public void Excluir(Usuario entity)
        {
            var query = @"
                DELETE USUARIO WHERE IDUSUARIO = @IdUsuario";

            using (var connection = new SqlConnection(_connecctionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Inserir(Usuario entity)
        {
            var query = @"
                INSERT INTO USUARIO(
                            IDUSUARIO, NOME, EMAIL, SENHA, DATACADASTRO, IDPERFIL)
                        VALUES(
                            @IdUsuario, @Nome, @Email, CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2), @DataCadastro, @IdPerfil)";
            using (var connection = new SqlConnection(_connecctionString))
            {
                connection.Execute(query, entity);
            }
        }

        public Usuario Obter(string email)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email";

            using (var connection = new SqlConnection(_connecctionString))
            {
                return connection.Query<Usuario>(query, new { email }).FirstOrDefault();
            }
        }

        public Usuario Obter(string email, string senha)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE EMAIL = @email
                AND SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @senha), 2)";

            using (var connection = new SqlConnection(_connecctionString))
            {
                return connection.Query<Usuario>(query, new { email, senha }).FirstOrDefault();
            }
        }

        public Usuario ObterPorId(Guid id)
        {
            var query = @"
                SELECT * FROM USUARIO
                WHERE IDUSUARIO = @id";

            using (var connection = new SqlConnection(_connecctionString))
            {
                return connection.Query<Usuario>(query, new {id}).FirstOrDefault();
            }
        }
    }
}
