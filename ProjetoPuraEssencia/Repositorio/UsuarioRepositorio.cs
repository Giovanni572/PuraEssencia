using MySql.Data.MySqlClient;
using ProjetoPuraEssencia.Models;
using System.Data;
using System.Runtime.ConstrainedExecution;

namespace ProjetoPuraEssencia.Repositorio
{
    public class UsuarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMysql = configuration.GetConnectionString("ConexaoMySql");

        public Usuario ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMysql))
            {
                conexao.Open();

                MySqlCommand cmd = new("SELECT * FROM usuario WHERE email = @email", conexao);
                cmd.Parameters.Add("email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;

                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            cpf = Convert.ToInt32(dr["cpf"]),
                            nome = dr["nome"].ToString(),
                            email = dr["email"].ToString(),
                            telefone = Convert.ToInt32(dr["telefone"]),
                            endereco = dr["endereco"].ToString(),
                            cep = Convert.ToInt32(dr["cep"]),
                            senha = dr["senha"].ToString(),
                            confirmarsenha = dr["confirmarsenha"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }

        public void RegistroUsuario(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMysql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand(
                    "INSERT INTO usuario (cpf, nome, email, telefone, endereco, cep, senha) " +
                    "VALUES(@cpf, @nome, @email, @telefone, @endereco, @cep, @senha)",
                    conexao
                );

                cmd.Parameters.Add("@cpf", MySqlDbType.Int32).Value = usuario.cpf;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.email;
                cmd.Parameters.Add("@telefone", MySqlDbType.Int32).Value = usuario.telefone;
                cmd.Parameters.Add("@endereco", MySqlDbType.VarChar).Value = usuario.endereco;
                cmd.Parameters.Add("@cep", MySqlDbType.Int32).Value = usuario.cep;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.senha;

                cmd.ExecuteNonQuery();
            }
        }

    }
}
