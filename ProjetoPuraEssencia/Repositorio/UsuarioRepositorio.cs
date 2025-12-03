using MySql.Data.MySqlClient;
using ProjetoPuraEssencia.Models;
using ProjetoPuraEssencia.Repositorio;
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
                            id_usuario = Convert.ToInt32(dr["id_usuario"]),
                            cpf = dr["cpf"].ToString(),
                            nome = dr["nome"].ToString(),
                            email = dr["email"].ToString(),
                            telefone = dr["telefone"].ToString(),
                            endereco = dr["endereco"].ToString(),
                            cep = dr["cep"].ToString(),
                            senha = dr["senha"].ToString(),
                            tipo = dr["tipo"].ToString()
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
                    "INSERT INTO usuario ( nome, cpf, email, senha, telefone, endereco, cep, tipo)" +
                    "VALUES(@nome, @cpf, @email, @senha, @telefone, @endereco, @cep, @tipo)",
                    conexao
                );

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.nome;
                cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = usuario.cpf;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.senha;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = usuario.telefone;
                cmd.Parameters.Add("@endereco", MySqlDbType.VarChar).Value = usuario.endereco;
                cmd.Parameters.Add("@cep", MySqlDbType.VarChar).Value = usuario.cep;
                cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = usuario.tipo ?? "Cliente";


                cmd.ExecuteNonQuery();
            }
        }

        //listas todos os clientes
        public IEnumerable<Usuario> TodosUsuarios()
        {
            List<Usuario> UsuarioList = new List<Usuario>();

            using (var conexao = new MySqlConnection(_conexaoMysql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * from usuario", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    UsuarioList.Add(
                                new Usuario
                                {
                                    id_usuario = Convert.ToInt32(dr["id_usuario"]),
                                    nome = ((string)dr["nome"]),
                                    telefone = ((string)dr["telefone"]),
                                    email = ((string)dr["email"]),
                                    tipo = ((string)dr["tipo"])
                                });
                }
                return UsuarioList;
            }
        }

        public void ExcluirUsuario(int id_usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMysql))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("delete from usuario where id_usuario=@id", conexao);

                cmd.Parameters.AddWithValue("@id", id_usuario);

                int i = cmd.ExecuteNonQuery();

                conexao.Close();
            }

        }
    }
}
