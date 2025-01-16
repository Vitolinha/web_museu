using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using web_museu.Models;
using web_museu.Repositorio;
using Newtonsoft.Json;

namespace web_museu.Controllers
{
    public class QuestionarioController : Controller
    {
        private readonly IQuestionarioRepositorio _repositorio;

        public QuestionarioController(IQuestionarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(QuestionarioModel model)
        {
            if (ModelState.IsValid)
            {
                // Recupera a sessão do usuário logado
                string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
                if (string.IsNullOrEmpty(sessaoUsuario))
                {
                    TempData["MensagemErro"] = "Sessão inválida. Faça login novamente.";
                    return RedirectToAction("Index", "Login");
                }

                // Deserializa a sessão para obter os dados do usuário logado
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                // Associa o ID do usuário logado ao modelo
                model.ContatoId = usuario.Id;

                // Adiciona o modelo ao repositório e salva no banco
                _repositorio.Adicionar(model);

                TempData["MensagemSucesso"] = "Avaliação enviada com sucesso!";
                return RedirectToAction("Index", "Home");
            }

            TempData["MensagemErro"] = "Erro ao enviar a avaliação. Verifique os campos.";
            return View("Index");
        }
    }
}
