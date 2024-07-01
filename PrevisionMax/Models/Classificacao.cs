using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public  class Classificacao
    {
        public int PosicaoTime { get; set; }
        public string NomeTime { get; set; } = string.Empty;
        public int numJogos { get; set; }
        public int numVitorias { get; set; }
        public int numEmpates { get; set; }
        public int numDerrotas { get; set; }
        public int GolsFeitos { get; set; }
        public int GolsSofridos{ get; set; }
        public int Pontos { get; set; }
        public string ultiomojogos1 { get; set; } = string.Empty;
        public string ultiomojogos2 { get; set; } = string.Empty;
        public string ultiomojogos3 { get; set; } = string.Empty;
        public string ultiomojogos4 { get; set; } = string.Empty;
        public string ultiomojogos5 { get; set; } = string.Empty;
    }
}
