using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public class EstatiscaTimes
    {
        public string NomeTime { get; set; } = string.Empty;
        public int Gols { get; set; }
        public int TentativasGols { get; set; }
        public int escanteios { get; set; }
        public int Inpedimentos { get; set; }
        public int Faltas { get; set; }
        public int CartoesVermelhos { get; set; }
        public int CartoesAmarelo { get; set; }

    }
}
