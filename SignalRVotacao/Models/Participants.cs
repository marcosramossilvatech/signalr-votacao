﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRVotacao.Models
{
    public class Participants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Código")]
        public int ParticId { get; set; }
        [DisplayName("Nome")]
        public string ParticName { get; set; }
        [DisplayName("Imagem")]
        public string Url { get; set; }
        [DisplayName("Total votos")]
        public int TotalVoto { get; set; }

        [NotMapped]
        public IFormFile Foto { get; set; }
    }
}
