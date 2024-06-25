using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AmigoChocolateBack.Models
{


    //public class Grupo
    //{
    //    public int? IDGrupo { get; set; }
    //    public string NomeGrupo { get; set; }
    //    public byte[]? Icone { get; set; }
    //    public int QuantidadeMaxima { get; set; }
    //    public decimal ValorChocolate { get; set; }
    //    public string? DataRevelacao { get; set; } //depois mudar para datetime
    //    public string Descricao { get; set; }


    //}

    public class Grupo
    {
        public int? IDGrupo { get; set; }
        public string NomeGrupo { get; set; }
        public int QuantidadeMaxima { get; set; }
        public decimal ValorChocolate { get; set; }
        public DateTime DataRevelacao { get; set; }  // Use DateTime aqui
        public string Descricao { get; set; }
        public string? Icone { get; set; }
        public ICollection<GrupoParticipante2>? GrupoParticipantes2 { get; set; }
        public ICollection<Participante>? Participantes { get; set; }
    }



    public class GrupoDto
    {
        public int? IDGrupo { get; set; }  // Adicionei IDGrupo, caso você precise
        public string NomeGrupo { get; set; }
        public int QuantidadeMaxima { get; set; }
        public decimal ValorChocolate { get; set; }
        public string DataRevelacao { get; set; } // Mantenha como string
        public string Descricao { get; set; }
        public string? Icone { get; set; }
        public ICollection<Participante>? Participantes { get; set; }
        public ICollection<GrupoParticipante2>? GrupoParticipantes2 { get; set; }
    }







}