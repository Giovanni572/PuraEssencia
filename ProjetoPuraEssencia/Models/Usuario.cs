namespace ProjetoPuraEssencia.Models
{
    public class Usuario
    {

        public int id_usuario { get; set; }
        public string? nome { get; set; }
        public string? cpf { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public string? tipo { get; set; }
        public string? telefone { get; set; }
        public string? endereco {  get; set; }
        public string? cep { get; set; }
        public string? confirmarsenha { get; set; }
        public List<Usuario>? ListaUsuario { get; set; }
    }
}
