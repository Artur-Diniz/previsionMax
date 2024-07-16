using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public class PartidaComEstatisticaDTO
    {
        public required Partidas Partida { get; set; }
        public required EstatisticaTimesCasa Casa { get; set; }
        public required EstatisticaTimesFora Fora { get; set; }
    }
}
