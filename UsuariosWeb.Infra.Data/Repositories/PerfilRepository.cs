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
    public class PerfilRepository : IPerfilRepository
    {
        private string _connecctionString;

        public PerfilRepository(string connecctionString)
        {
            _connecctionString = connecctionString;
        }

        public void Alterar(Perfil entity)
        {
            var query = @"
                UPDATE PERFIL SET NOME = @Nome
                WHERE IDPERFIL = @IdPerfil";

            using (var connection = new SqlConnection(_connecctionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Perfil> Consultar()
        {
            var query = @"
                SELECT * FROM PERFIL
                ORDER BY NOME";

            using (var connection = new SqlConnection(_connecctionString))
            {
                return connection.Query<Perfil>(query).ToList();
            }
        }

        public void Excluir(Perfil entity)
        {
            var query = @"
                DELETE FROM PERFIL
                WHERE IDPERFIL = @IdPerfil";

            using (var connection = new SqlConnection(_connecctionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Inserir(Perfil entity)
        {
            var query = @"
                INSERT INTO PERFIL(IDPERFIL, NOME)
                VALUES(@IdPerfil, @Nome)";

            using (var connection = new SqlConnection(_connecctionString))
            {
                connection.Execute(query, entity);
            }
        }

        public Perfil Obter(string nome)
        {
            var query = @"
                SELECT * FROM PERFIL
                WHERE NOME = @nome";
            using (var connection = new SqlConnection(_connecctionString))
            {
                return connection.Query<Perfil>(query, new { nome }).FirstOrDefault();
            }
        }

        public Perfil ObterPorId(Guid id)
        {
            var query = @"
                SELECT * FROM PERFIL
                WHERE IDPERFIL = @id";

            using (var connection = new SqlConnection(_connecctionString))
            {
                return connection.Query<Perfil>(query, new {id}).FirstOrDefault();
            }
        }
    }
}
