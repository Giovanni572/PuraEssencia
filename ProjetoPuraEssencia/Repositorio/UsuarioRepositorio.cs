using System.Data;
using MySql.Data.MySqlClient;
using ProjetoPuraEssencia.Models;

namespace ProjetoPuraEssencia.Repositorio
{
    public class UsuarioRepositorio(IConfiguration configuration)
    {
        //declarando variavel de leitura de conexao
        private readonly string _conexaoMysql = configuration.GetConnectionString("ConexaoMysql");

        public Usuario ObterUsuario(String email) 
        { 
            using (var conexao = new MySqlConnection(_conexaoMysql)) 
            {
                conexao.Open();
                MySqlCommand cmd = new("select * from usuario where email = @email", conexao);
                cmd.Parameters.Add("email", MySqlDbType.VarChar).Value = email;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString(),
                            Email = dr["Email"].ToString(),
                            Senha = dr["Senha"].ToString()
                        };
                        return usuario;
                    }
                }
            }
            return null;
        }
    }
}
