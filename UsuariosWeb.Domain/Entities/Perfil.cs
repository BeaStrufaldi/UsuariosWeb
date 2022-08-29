using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosWeb.Domain.Entities
{
    public class Perfil
    {
        #region Propriedades
        public Guid IdPerfil { get; set; }
        public string Nome { get; set; }
        #endregion

        #region Relacionamentos
        public List<Usuario> Usuarios { get; set; }
        #endregion

    }
}
