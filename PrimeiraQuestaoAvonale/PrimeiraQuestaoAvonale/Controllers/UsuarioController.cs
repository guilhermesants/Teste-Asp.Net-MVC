using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PrimeiraQuestaoAvonale.Models;
using PrimeiraQuestaoAvonale.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace PrimeiraQuestaoAvonale.Controllers
{
    [Route("Usuario")]
    public class UsuarioController : Controller
    {

        static string user = null;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUsuario()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Repositorios(string Pesquisar)
        {

            user = Pesquisar;

            string repositorio = $"https://api.github.com/users/{Pesquisar}/repos";

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
                return RedirectToAction("GetUsuario", "Usuario");
            }
            


            return View(repositorios);
        }

        [HttpGet]
        public async Task<ActionResult> DetalhesUsuario(string name)
        {
            string repositorio = $"https://api.github.com/repos/{user}/{name}";

            DetalheRepositorioViewModel detalhes = null;

            HttpResponseMessage resposta = await GetUri.Uri(repositorio);

            if (resposta.IsSuccessStatusCode)
            {
                var conteudo = await resposta.Content.ReadAsStringAsync();

                detalhes = JsonConvert.DeserializeObject<DetalheRepositorioViewModel>(conteudo);

                // pegando apenas a data
                var data = detalhes.updated_at.Substring(0, 10);

                detalhes.updated_at = data;
            }
            return View(detalhes);
        }

        public ActionResult MarcarFavorito(string proprietario, string nome)
        {
            var nomeRepositorio = proprietario +@"/"+ nome;

            var jaExiste = Repository.ExistElement(nomeRepositorio);

            if ((nomeRepositorio != null) && (!jaExiste))
            {
                Repository.Add(nomeRepositorio);
            }
            return RedirectToAction("GetUsuario", "Usuario");
        }
    }
}