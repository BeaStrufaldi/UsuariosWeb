namespace UsuariosWeb.Presentation.Models
{
    public class UsuariosConsultaModel
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
