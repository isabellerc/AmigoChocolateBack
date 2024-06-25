namespace AmigoChocolateBack.Models
{
    public class GrupoParticipante2
    {
        public int IDGrupoParticipante { get; set; } // Chave primária auto-incrementada

        public int IDGrupo { get; set; } // Chave estrangeira para Grupo
        public Grupo Grupo { get; set; } // Propriedade de navegação

        public int IDParticipante { get; set; } // Chave estrangeira para Participante
        public Participante Participante { get; set; } // Propriedade de navegação
    }

    public class GrupoParticipante2Dto
    {
        public int IDGrupoParticipante { get; set; }
        public int IDGrupo { get; set; }
        public int IDParticipante { get; set; }
    }
}
