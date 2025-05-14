using Microsoft.AspNetCore.Http.HttpResults;

namespace LojaBDS.Models
{
    public class Prdouto
    {

        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal PrecoProduto { get; set; }
        public int QuantidadeProduto { get; set; }

    }
}