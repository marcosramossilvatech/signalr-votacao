using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace SignalRVotacao.Models
{
    public class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Código")]
        public int VoteId { get; set; }
        [DisplayName("Periodo")]
        public string Periodo { get; set; }

        [DisplayName("Data Inicio")]
        public DateTime DataInicio { get; set; }

        [DisplayName("Data Fim")]
        public DateTime DataFim{ get; set; }

        [DisplayName("Participante 1")]
        [ForeignKey("Participants")]
        [Column(Order = 1)]
        public int Participant1Id { get; set; }
        public virtual Participants Participants { get; set; }

        [DisplayName("Total participante 1")]
        public int Participant1Total { get; set; }

        [DisplayName("Participante 2")]
        [ForeignKey("Participants2")]
        [Column(Order = 2)]
        public int Participant2Id { get; set; }
        public virtual Participants Participants2 { get; set; }

        [DisplayName("Total participante 2")]
        public int Participant2Total { get; set; }



        [DisplayName("Participante 3")]
        [ForeignKey("Participants3")]
        [Column(Order = 3)]
        public int Participant3Id { get; set; }
        public virtual Participants Participants3 { get; set; }

        [DisplayName("Total participante 3")]
        public int Participant3Total { get; set; }

    }
}
