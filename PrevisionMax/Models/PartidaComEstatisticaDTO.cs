using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public class PartidaComEstatisticaDTO
    {
        public  Partidas Partida { get; set; }
        public  EstatisticaTimesCasa Casa { get; set; }
        public  EstatisticaTimesFora Fora { get; set; }
    }
}
