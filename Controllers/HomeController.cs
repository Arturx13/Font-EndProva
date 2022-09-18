using Font_End.Models;
using Font_End.Models.Veiculos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Font_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> index()
        {

            return View();
        }
        public async Task<IActionResult> Crud()
        {

            ViewBag.Marcas = new Marcas().Lista.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SalvarVeiculo(string veiculo, string descricao, string marca, int ano, bool vendido, int id = 0)
        {

            Veiculo Registro = new Veiculo{
                id = id,
                veiculo = veiculo,
                descricao = descricao,
                marca = marca,
                ano = ano,
                vendido = vendido,
            };



            RestResponse response = new RestResponse();

            if(!new Marcas().Lista.Contains(Registro.marca.ToUpper()))
                return Json(new { result = "Marca Inexistente!" });

            if (id == 0)
            {
                var client = new RestClient("https://localhost:44313");
                var request = new RestRequest("/api/Veiculos", Method.Post);
                request.AddParameter("application/json", Registro, ParameterType.RequestBody);
                 response = await client.ExecutePostAsync(request);
            }
            else
            {
                var client = new RestClient("https://localhost:44313");
                var request = new RestRequest("/api/Veiculos", Method.Patch);
                request.AddParameter("application/json", Registro, ParameterType.RequestBody);
                response = await client.PatchAsync(request);
            }
     

            Veiculo obj = JsonConvert.DeserializeObject<Veiculo>(response.Content);


            return Json(new { obj });
        }

        public async Task<IActionResult> ResultadoVotos()
        {

            var client = new RestClient("https://localhost:44313");
            var request = new RestRequest("/api/Veiculos/ResultadoVotos", Method.Get);
            var response = await client.GetAsync(request);

            PercentualVotos obj = JsonConvert.DeserializeObject<PercentualVotos>(response.Content);


            return Json(new { obj });
        }
        public async Task<IActionResult> SomaMultiplos(int numero)
        {

            var client = new RestClient("https://localhost:44313");
            var request = new RestRequest("/api/Veiculos/SomaMultiplos", Method.Get).AddParameter("numero", numero);
            var response = await client.GetAsync(request);

            Multiplos obj = JsonConvert.DeserializeObject<Multiplos>(response.Content);


            return Json(new { obj });
        }


        public async Task<IActionResult> RetonaFatorial(int numero)
        {

            var client = new RestClient("https://localhost:44313");
            var request = new RestRequest("/api/Veiculos/RetonaFatorial", Method.Get).AddParameter("numero", numero);
            var response = await client.GetAsync(request);

            Fatorial obj = JsonConvert.DeserializeObject<Fatorial>(response.Content);


            return Json(new { obj });
        }
        public async Task<IActionResult> BubbleSort()
        {

            var client = new RestClient("https://localhost:44313");
            var request = new RestRequest("/api/Veiculos/BubbleSort", Method.Get);
            var response = await client.GetAsync(request);

            BubleResultado obj = JsonConvert.DeserializeObject<BubleResultado>(response.Content);



            return Json(new { obj });
        }


        public async Task<IActionResult> _TabelaVeiculos(string marca = "",int ano = 0 , string cor = "")
        {

            var client = new RestClient("https://localhost:44313");
            var request = new RestRequest("/api/Veiculos", Method.Get).AddParameter("marca", marca).AddParameter("ano", ano).AddParameter("cor", cor);
            var response = await client.GetAsync(request);

            List<Veiculo> obj = JsonConvert.DeserializeObject<List<Veiculo>>(response.Content);


            return PartialView(obj);
        }


        public async Task<IActionResult> Edit(int id)
        {

            var client = new RestClient("https://localhost:44313");
            var request = new RestRequest("/api/Veiculos", Method.Get).AddParameter("id", id);
            var response = await client.GetAsync(request);

            Veiculo obj = JsonConvert.DeserializeObject<Veiculo>(response.Content);

            return Json(new { obj });
          
        }
        public async Task<IActionResult> Excluir(string id)
        {

            var client = new RestClient("https://localhost:44313");
            var request = new RestRequest("/api/Veiculos", Method.Delete).AddParameter("id", id);
            var response = await client.DeleteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return Json(new { result = true });
            else
                return Json(new { result = false });
        }







    }
}
