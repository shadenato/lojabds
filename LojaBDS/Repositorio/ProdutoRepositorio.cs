using MySql.Data.MySqlClient;
using System.Data;
using LojaBDS.Models;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using MySqlX.XDevAPI;

namespace LojaBDS.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Produto (nomeProduto, descricaoProduto, precoProduto, quantidadeProduto) values(@nome, @descricao, @preco, @quantidade)", conexao);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nomeProduto;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricaoProduto;
                cmd.Parameters.Add("@preco", MySqlDbType.VarChar).Value = produto.precoProduto;
                cmd.Parameters.Add("@quantidade", MySqlDbType.VarChar).Value = produto.quantidadeProduto;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update produto set nomeProduto=@nome, descricaoProduto=@descricao, precoProduto=@preco, quantidadeProduto=@quantidade" + " where idProduto=@id ", conexao);
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = produto.idProduto;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nomeProduto;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricaoProduto;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.precoProduto;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidadeProduto;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> ProdutoLista = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ProdutoLista.Add(
                                new Produto
                                {
                                    idProduto = Convert.ToInt32(dr["idProduto"]),
                                    nomeProduto = ((string)dr["nomeProduto"]),
                                    descricaoProduto = ((string)dr["descricaoProduto"]),
                                    precoProduto = Convert.ToDecimal(dr["precoProduto"]),
                                    quantidadeProduto = Convert.ToInt32(dr["quantidadeProduto"]),
                                });
                }
                return ProdutoLista;
            }
        }

        public Produto ObterProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produto WHERE idProduto=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Produto produto = new Produto();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.idProduto = Convert.ToInt32(dr["idProduto"]);
                    produto.nomeProduto = (string)(dr["nomeProduto"]);  
                    produto.descricaoProduto = (string)(dr["descricaoProduto"]);
                    produto.precoProduto = Convert.ToDecimal(dr["precoProduto"]);
                    produto.quantidadeProduto = Convert.ToInt32(dr["quantidadeProduto"]);
                }
                return produto;
            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Produto WHERE idProduto=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
