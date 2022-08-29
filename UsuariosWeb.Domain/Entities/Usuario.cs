using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosWeb.Domain.Entities
{
    public class Usuario
    {
        #region Propriedades
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid IdPerfil { get; set; }

        #endregion

        #region Relacionamentos
        public Perfil Perfil { get; set; }
        #endregion
    }
}
