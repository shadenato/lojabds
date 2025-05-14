using Microsoft.AspNetCore.Http.HttpResults;

namespace LojaBDS.Models
{
    public class Prdouto
    {

        public int idProduto { get; set; }
        public string nomeProduto { get; set; }
        public string descricaoProduto { get; set; }
        public decimal precoProduto { get; set; }
        public int quantidadeProduto { get; set; }

    }
}