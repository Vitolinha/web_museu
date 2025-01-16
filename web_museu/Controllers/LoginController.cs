using Microsoft.AspNetCore.Mvc;
using web_museu.Enums;
using web_museu.Helper;
using web_museu.Models;

namespace web_museu.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            // Se usuário estiver logado, redirecionar para a Home
            if (_sessao.BuscarSessaoDoUsuario() != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar se o login é do usuário mestre
                    if (loginModel.Login == "admin" && loginModel.Senha == "master123")
                    {
                        // Criar manualmente o usuário mestre
                        var usuarioMestre = new UsuarioModel
                        {
                            Id = 0, // ID fictício
                            Nome = "Usuário Mestre",
                            Login = "admin",
                            Email = "gjesusdantas2@gmail.com",
                            Perfil = PerfilEnum.Admin, // Ajuste de acordo com seu enum
                            DataCadastro = DateTime.Now
                        };

                        // Criar sessão para o usuário mestre
                        _sessao.CriarSessaoDoUsuario(usuarioMestre);

                        // Redirecionar para a Home
                        return RedirectToAction("Index", "Home");
                    }

                    // Verificar no banco de dados
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            // Criar sessão para o usuário encontrado no banco
                            _sessao.CriarSessaoDoUsuario(usuario);

                            // Redirecionar para a Home
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = "Senha do usuário é inválida, tente novamente.";
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Usuário ou senha inválidos.";
                    }
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                // Tratamento de erro
                TempData["MensagemErro"] = $"Erro ao processar a solicitação: {erro.Message}";
                return View("Index");
            }
        }


        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        usuario.Senha = novaSenha;
                        _usuarioRepositorio.Atualizar(usuario);

                        // Enviar o e-mail com a nova senha
                        string assunto = "Redefinição de Senha - AstroMarte";
                        string mensagem = $"Olá {usuario.Nome},\n\nSua nova senha é: {novaSenha}\nPor favor, altere-a após o login.";
                        bool emailEnviado = _email.Enviar(usuario.Email, assunto, mensagem);

                        if (emailEnviado)
                        {
                            TempData["MensagemSucesso"] = "Um link para redefinir sua senha foi enviado para o e-mail cadastrado.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Erro ao enviar o e-mail. Por favor, tente novamente.";
                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = "Não conseguimos redefinir sua senha. Por favor, verifique os dados informados e tente novamente.";
                }
                else
                {
                    TempData["MensagemErro"] = "Dados inválidos. Verifique e tente novamente.";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha. Tente novamente. Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
