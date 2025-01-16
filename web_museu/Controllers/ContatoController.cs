using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using X.PagedList;
using web_museu.Models;
using web_museu.Repositorio;
using web_museu.Services;
using X.PagedList.Extensions;

namespace web_museu.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly EmailService _emailService;

        public ContatoController(IContatoRepositorio contatoRepositorio, EmailService emailService)
        {
            _contatoRepositorio = contatoRepositorio ?? throw new ArgumentNullException(nameof(contatoRepositorio));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public IActionResult Index(int? pagina, string busca)
        {
            const int tamanhoPagina = 10;
            int numeroPagina = pagina ?? 1;

            var contatos = _contatoRepositorio.BuscarTodos() ?? new List<ContatoModel>();

            if (!string.IsNullOrEmpty(busca))
            {
                contatos = contatos
                    .Where(c => c.Nome.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                                c.Email.Contains(busca, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var contatosPaginados = contatos.ToPagedList(numeroPagina, tamanhoPagina);

            ViewBag.BuscaAtual = busca;

            return View(contatosPaginados);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(ContatoModel contato)
        {
            if (ModelState.IsValid)
            {
                contato.DataCriacao = DateTime.Now; // Garantir que a DataCriacao seja preenchida
                _contatoRepositorio.Adicionar(contato);
                TempData["MensagemSucesso"] = "Contato criado com sucesso!";
                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Erro ao criar o contato. Verifique os dados.";
            return View(contato);
        }

        public IActionResult Editar(int id)
        {
            var contato = _contatoRepositorio.BuscarPorId(id);
            if (contato == null)
            {
                TempData["MensagemErro"] = "Contato não encontrado.";
                return NotFound();
            }
            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ContatoModel contato)
        {
            if (ModelState.IsValid)
            {
                var contatoExistente = _contatoRepositorio.BuscarPorId(contato.IdContato);
                if (contatoExistente == null)
                {
                    TempData["MensagemErro"] = "Contato não encontrado.";
                    return NotFound();
                }

                contatoExistente.Nome = contato.Nome;
                contatoExistente.Email = contato.Email;
                contatoExistente.Celular = contato.Celular;

                _contatoRepositorio.Atualizar(contatoExistente);

                TempData["MensagemSucesso"] = "Contato atualizado com sucesso!";
                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Erro ao atualizar o contato. Verifique os dados.";
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            var contato = _contatoRepositorio.BuscarPorId(id);
            if (contato == null)
            {
                TempData["MensagemErro"] = "Contato não encontrado.";
                return NotFound();
            }
            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apagar(int id)
        {
            try
            {
                var sucesso = _contatoRepositorio.Deletar(id);
                if (sucesso)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                    return RedirectToAction("Index");
                }

                TempData["MensagemErro"] = "Erro ao apagar o contato.";
                return View("Erro");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao apagar o contato: {ex.Message}";
                return View("Erro");
            }
        }
    }
}
