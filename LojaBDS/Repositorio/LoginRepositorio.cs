using MySql.Data.MySqlClient;
using System.Data;
using LojaBDS.Models;
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace LojaBDS.Repositorio
{
    public class LoginRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public Usuario ObterUsuario(string emailUsuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM Usuario where EmailUsuario = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailUsuario;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;

                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            idUsuario = Convert.ToInt32(dr["idUsuario"]),
                            nomeUsuario = dr["nomeUsuario"].ToString(),
                            emailUsuario = dr["emailUsuario"].ToString(),
                            senhaUsuario = dr["senhaUsuario"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }
    }
}
