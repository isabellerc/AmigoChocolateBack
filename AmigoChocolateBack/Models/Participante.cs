using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmigoChocolateBack.Models
{
    public class Participante
    {

        [Key]
        public int IDParticipante { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeParticipante { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailParticipante { get; set; }

        [Required]
        [StringLength(100)]
        public string SenhaParticipante { get; set; }

        // Propriedade de navegação para os grupos que o participante pertence
        public ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();
        public ICollection<GrupoParticipante2> GrupoParticipantes2 { get; set; }

    }

    public class ParticipanteDto
    {
        public int IDParticipante { get; set; }
        public string NomeParticipante { get; set; }
        public string EmailParticipante { get; set; }
        
    }
}
