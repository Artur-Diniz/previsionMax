using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrevisionMax.Models;

namespace PrevisionMax.Transicao
{
    public static class EnviarPartida
    {
        public static async Task EnviarDadosAsync(PartidaComEstatisticaDTO dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var url = "http://localhost:5130/api/Partidas/PartidaComEstatistica";
                var response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Dados enviados com sucesso!");
                }
                else
                {
                    Console.WriteLine("Falha ao enviar os dados");
                }
            }
        }
    }
}
