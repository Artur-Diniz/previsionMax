using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public class Partidas
    {
    
        public int idPartida { get; set; }
        public string NomeTimeCasa { get; set; } = string.Empty;
        public string NomeTimeFora { get; set; } = string.Empty;
        public DateTime data { get; set; }

        public bool PartidaAnalise { get; set; }

        public string Campeonato { get; set; } = string.Empty;

        [ForeignKey("EstatisticaCasa")]
        public int IdEstatisticaCasa { get; set; }

        [ForeignKey("EstatisticaFora")]
        public int IdEstatisticaFora { get; set; }

    }
}
