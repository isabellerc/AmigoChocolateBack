﻿using System;
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
        public int IDGrupo { get; set; }
        public string NomeGrupo { get; set; }
        public int QuantidadeMaxima { get; set; }
        public decimal ValorChocolate { get; set; }
        public DateTime DataRevelacao { get; set; }
        public string Descricao { get; set; }
        public string? Icone { get; set; }
    }

    public class GrupoDto
    {
        public string NomeGrupo { get; set; }
        public int QuantidadeMaxima { get; set; }
        public decimal ValorChocolate { get; set; }
        public DateTime DataRevelacao { get; set; }
        public string Descricao { get; set; }
        //public IFormFile Icone { get; set; }
        public string? Icone { get; set; }
    }




}
