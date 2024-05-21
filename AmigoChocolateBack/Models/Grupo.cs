using System;
namespace AmigoChocolateBack.Models
{
    

        public class Grupo
        {
            public int IDGrupo { get; set; }
            public string NomeGrupo { get; set; }
            public byte[]? Icone { get; set; }
            public int QuantidadeMaxima { get; set; }
            public decimal ValorChocolate { get; set; }
            public DateTime DataRevelacao { get; set; }
            public string Descricao { get; set; }
        }
    
}
