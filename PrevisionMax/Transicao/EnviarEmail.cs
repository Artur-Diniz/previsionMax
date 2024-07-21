using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrevisionMax.Transicao
{
    public static class EnviarEmail
    {
        public static async Task EnviarDadosAsync()
        {
            using (var client = new HttpClient())
            {
                // Defina um timeout maior, se necessário
                client.Timeout = TimeSpan.FromSeconds(30);

                try
                {
                    var url = "https://localhost:7038/api/palpites/GerarPalpites";
                    Console.WriteLine($"Enviando requisição para {url}");

                    // Tente enviar um JSON vazio, caso a API espere algum conteúdo
                    string json = "nada não";
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    // Adicione headers, se necessário
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Envie a requisição
                    var response = await client.PostAsync(url, data);
                    Console.WriteLine("Requisição enviada, aguardando resposta...");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Dados enviados com sucesso!");
                    }
                    else
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Falha ao enviar os dados. Código de status: {response.StatusCode}, Erro: {errorResponse}");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro de requisição HTTP: {e.Message}");
                    if (e.InnerException != null)
                    {
                        Console.WriteLine($"Detalhes internos: {e.InnerException.Message}");
                    }
                }
                catch (TaskCanceledException e)
                {
                    Console.WriteLine("A requisição foi cancelada (provavelmente um timeout): " + e.Message);
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine("A operação foi cancelada: " + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Erro inesperado: {e.Message}");
                }
            }
        }
    }
}
