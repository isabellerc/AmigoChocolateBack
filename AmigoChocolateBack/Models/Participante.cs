using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
