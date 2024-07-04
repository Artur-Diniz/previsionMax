using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevisionMax.Models
{
    public class EstatisticaTimesCasa
    {

            public int IdEstatisticaCasa { get; set; }
            public string NomeTimeCasa { get; set; } = string.Empty;
            public int GolsCasa { get; set; }
            public int GolsSofridosCasa { get; set; }
            public string AdversarioFora { get; set; } = string.Empty;
            public int TentativasGolsCasa { get; set; } // tentativas de Gol
            public int chutesnoGolsCasa { get; set; }//chutes no gol 
            public int chutespraforaCasa { get; set; }//Chutes para Fora

            public int escanteiosCasa { get; set; }//Escanteios
            public int InpedimentosCasa { get; set; }// Impedimentos
            public int DefesaGoleiroCasa { get; set; }//Defesas do Goleiro
            public int FaltasCasas { get; set; }
            public int CartoesVermelhosCasa { get; set; }// Cartões Vermelhos
            public int CartoesAmareloCasa { get; set; }// Cartões Amarelos
            public int PassesTotaisCasa { get; set; }// Passes Totais
            public int PassesCompletosCasa { get; set; }//  Passes Completados
            public int AtaquesperigososCasa { get; set; } // Ataques Perigosos


        
    }
}
