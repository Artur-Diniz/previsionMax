using PrevisionMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Driver
{
    public  class Email
    {
        public Email()
        {
            PartidaComEstatisticaDTO nada = new();
            Transicao.EnviarEmail.EnviarDadosAsync();
        }
    }
}
