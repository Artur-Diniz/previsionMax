using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public class CampeonatoDTO
    {
        public List<Classificacao> TimesClassificao { get; set; }
        public TabelaCampeonato Campeonato { get; set; }
    }
}
