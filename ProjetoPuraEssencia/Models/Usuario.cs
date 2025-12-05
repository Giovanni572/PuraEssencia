using System.ComponentModel.DataAnnotations;

namespace ProjetoPuraEssencia.Models
{
    public class Usuario
    {

        [Display(Name = "Id")]
        public int id_usuario { get; set; }
        [Display(Name = "Nome")]
        public string? nome { get; set; }
        public string? cpf { get; set; }
        [Display(Name = "E-mail")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Informe a senha.")]
        public string? senha { get; set; }
        [Display(Name = "Tipo")]
        public string? tipo { get; set; }
        [Display(Name = "Telefone")]
        public string? telefone { get; set; }
        public string? endereco {  get; set; }
        public string? cep { get; set; }
        [Required(ErrorMessage = "Confirme sua senha.")]
        [Compare("senha", ErrorMessage = "As senhas não coincidem.")]
        public string? confirmarsenha { get; set; }
        public List<Usuario>? ListaUsuario { get; set; }
    }
}
