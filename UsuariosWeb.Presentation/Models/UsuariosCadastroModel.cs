using System.ComponentModel.DataAnnotations;

namespace UsuariosWeb.Presentation.Models
{
    public class UsuariosCadastroModel
    {
        [MinLength(6, ErrorMessage = "Preencha o minimo {1} de caracteres")]
        [MaxLength(150, ErrorMessage = "Preencha o max {1} de caracteres")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Preencha o email")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Email { get; set; }

        [MinLength(8, ErrorMessage = "Preencha o min {1} de caracteres")]
        [MaxLength(20, ErrorMessage = "Preencha o max {1} de caracteres")]
        [Required(ErrorMessage = "Obrigatório")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
        [Required(ErrorMessage = "Obrigatório")]
        public string SenhaConfirmacao { get; set; }
    }
}
