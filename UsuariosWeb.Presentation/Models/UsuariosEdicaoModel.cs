using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace UsuariosWeb.Presentation.Models
{
    public class UsuariosEdicaoModel
    {
        public Guid IdUsuario { get; set; }

        [Required (ErrorMessage = "Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string IdPerfil { get; set; }

        public List<SelectListItem>? ListagemPerfis { get; set; }
    }
}
