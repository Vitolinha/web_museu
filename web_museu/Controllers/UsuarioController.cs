using Microsoft.AspNetCore.Mvc;
using web_museu.Models;
using web_museu.Repositorio;
using X.PagedList;
using System;
using X.PagedList.Extensions;
using web_museu.Services;

namespace web_museu.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly EmailService _emailService;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio ?? throw new ArgumentNullException(nameof(usuarioRepositorio));
        }

        // Método Index - Listagem de Usuários
        public IActionResult Index(int? pagina, string busca)
        {
            const int tamanhoPagina = 10; // Define o número de itens por página
            int numeroPagina = pagina ?? 1; // Página atual (padrão é 1)

            // Busca todos os usuários, garantindo que nunca será null
            var usuarios = _usuarioRepositorio.BuscarTodos() ?? new List<UsuarioModel>();

            // Filtro de busca
            if (!string.IsNullOrEmpty(busca))
            {
                busca = busca.Trim(); // Remove espaços desnecessários
                usuarios = usuarios
                    .Where(u => u.Nome.Contains(busca, StringComparison.OrdinalIgnoreCase) ||
                                u.Email.Contains(busca, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Aplicar paginação
            var usuariosPaginados = usuarios.ToPagedList(numeroPagina, tamanhoPagina);

            // Enviar valor da busca para a view
            ViewBag.BuscaAtual = busca;

            return View(usuariosPaginados);
        }
        [HttpPost]
        public async Task<IActionResult> EnviarEmailCadastro(string email)
        {
            var subject = "Bem-vindo ao AstroMarte!";
            var body = "<h1>Obrigado por se cadastrar!</h1><p>Agora você faz parte da nossa missão.</p>";

            await _emailService.SendEmailAsync(email, subject, body);

            return RedirectToAction("Index");
        }


        // Método Criar - Exibir Formulário
        public IActionResult Criar()
        {
            return View();
        }

        // Método Criar - Salvar Novo Usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário criado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["MensagemErro"] = $"Ocorreu um erro ao cadastrar o usuário: {ex.Message}";
                }
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao cadastrar o usuário. Verifique os dados e tente novamente.";
            }

            return View(usuario);
        }

        // Método Editar - Exibir Formulário para Edição
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            if (usuario == null)
            {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // Método Editar - Atualizar Usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Cria uma instância do modelo com os dados recebidos
                    UsuarioModel usuario = new UsuarioModel
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil,
                        DataAtualizacao = DateTime.Now
                    };

                    _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                    return RedirectToAction("Index");
                }

                TempData["MensagemErro"] = "Ocorreu um erro ao atualizar o usuário. Verifique os dados.";
                return View(usuarioSemSenhaModel);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao atualizar o usuário: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // Método Apagar - Confirmação
        public IActionResult ApagarConfirmacao(int id)
        {
            var usuario = _usuarioRepositorio.BuscarPorId(id);
            if (usuario == null)
            {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // Método Apagar - Remover Usuário
        public IActionResult Apagar(int id)
        {
            try
            {
                var sucesso = _usuarioRepositorio.Apagar(id);
                if (sucesso)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
                    return RedirectToAction("Index");
                }

                TempData["MensagemErro"] = "Ocorreu um erro ao tentar apagar o usuário.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro ao apagar o usuário: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
