using Newtonsoft.Json;
using PrimeiraQuestaoAvonale.Models;
using PrimeiraQuestaoAvonale.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PrimeiraQuestaoAvonale.Controllers
{
    [Route("MeuRepositorio")]
    public class MeuRepositorioController : Controller
    {

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            string repositorio = "https://api.github.com/users/guilhermesants/repos";

            IEnumerable<MeusRepositoriosViewModel> repositorios = null;

            HttpResponseMessage resposta = await GetUri.Uri(repositorio);

            if (resposta.IsSuccessStatusCode)
            {
                var conteudo = await resposta.Content.ReadAsStringAsync();
                repositorios = JsonConvert.DeserializeObject<MeusRepositoriosViewModel[]>(conteudo);
            }
            else
            {
                repositorios = Enumerable.Empty<MeusRepositoriosViewModel>();
                ModelState.AddModelError(string.Empty, "Repositórios não encontrados");
            }

            return View(repositorios);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Detalhes(string name)
        {
            string repositorio = $"https://api.github.com/repos/guilhermesants/{name}";

            DetalheRepositorioViewModel detalhes = null;

            HttpResponseMessage resposta = await GetUri.Uri(repositorio);

            if (resposta.IsSuccessStatusCode)
            {
                var conteudo = await resposta.Content.ReadAsStringAsync();

                detalhes = JsonConvert.DeserializeObject<DetalheRepositorioViewModel>(conteudo);

                // pegando apenas a data a partir de uma substring
                var data = detalhes.updated_at.Substring(0, 10);
                detalhes.updated_at = data;
            }
            return View(detalhes);
        }

        public ActionResult MarcarFavorito(string proprietario, string nome)
        {
            var nomeRepositorio = proprietario + @"/" + nome;

            var jaExiste = Repository.ExistElement(nomeRepositorio);

            if ((nomeRepositorio != null) && (!jaExiste))
            {
                Repository.Add(nomeRepositorio);
            }
            return RedirectToAction("Index", "MeuRepositorio");
        }

        public ActionResult ListarFavoritos()
        {
            var conteudo = Repository.GetAll();

            return View(conteudo.ToList());
        }
    }
}