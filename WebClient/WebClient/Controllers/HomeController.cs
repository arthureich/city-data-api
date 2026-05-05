using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Cidade> cidades = new List<Cidade>(); 
             using (var httpClient = new HttpClient()) {
                try
                {
                    using (var resposta = await httpClient.GetAsync(
                        $"https://localhost:5001/paises/55/estado/11/cidades"))
                    {
                        if (resposta.IsSuccessStatusCode)
                        {
                            var conteudo = await resposta.Content.ReadAsStringAsync();

                            if (!string.IsNullOrEmpty(conteudo))
                            {
                            cidades = JsonSerializer.Deserialize<List<Cidade>>(conteudo, new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            });
                        }
                        else
                    {
                        ViewBag.ErroBusca = "Erro na busca";
                    }
                }
                }
                }
                catch (Exception)
                {
                    ViewBag.ErroBusca = "Erro na busca";
                }
            }
            return View(cidades);
        }
        [HttpPost]  
        public async Task<IActionResult> Index(int idCidade, string nome, int estado, int pais, int populacao)
        {
            Cidade cidade= new Cidade() {
                Id = idCidade,
                Nome = nome,
                Populacao = populacao         };

            using (var httpClient = new HttpClient())
            {
                StringContent conteudo = new StringContent(
                    JsonSerializer.Serialize(cidade),
                    Encoding.UTF8,
                    "application/json"
                );
                try
                {
                    using (var resposta = await httpClient.PostAsync("https://localhost:5001/paises/{pais}/estado/{estado}/cidades", conteudo))
                    {
                        if (resposta.IsSuccessStatusCode)
                        {
                            ViewBag.MensagemGravacao = "Gravado com sucesso";
                        }
                        else
                    {
                        ViewBag.MensagemGravacao = "Erro Gravacao";
                    }
                }
                }
                catch (Exception e)
                {
                    ViewBag.MensagemGravacao = "Erro Gravacao";
                }
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
