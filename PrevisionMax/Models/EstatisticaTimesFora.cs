using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public class EstatisticaTimesFora
    {
        public int IdEstatisticaFora{ get; set; }
        public string NomeTimeFora { get; set; } = string.Empty;
        public int GolsFora { get; set; }
        public int TentativasGolsFora { get; set; }
        public int chutesnoGolsFora { get; set; }
        public int chutespraforaFora { get; set; }
        public int escanteiosFora { get; set; }
        public int InpedimentosFora { get; set; }
        public int DefesaGoleiroFora { get; set; }
        public int FaltasForas { get; set; }
        public int CartoesVermelhosFora { get; set; }
        public int CartoesAmareloFora { get; set; }
        public int PassesTotaisFora { get; set; }
        public int PassesCompletosFora { get; set; }
        public int AtaquesperigososFora { get; set; }

    }
}
