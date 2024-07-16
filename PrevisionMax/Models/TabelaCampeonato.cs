using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public class TabelaCampeonato
    {
     
        public int idCampeonato { get; set; }
        public string NomeCampeonato { get; set; } = string.Empty;
        public DateTime data { get; set; }
        public ICollection<int>? ClassificacaoIds { get; set; } = new List<int>();
    }
}
