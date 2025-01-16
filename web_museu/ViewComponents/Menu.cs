using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using web_museu.Models;

namespace web_museu.ViewComponents
{
    public class Menu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Obtém a sessão do usuário logado
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                // Retorna um menu padrão ou vazio
                return View("Default");
            }

            // Deserializa o objeto do usuário logado
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

            // Retorna a view com o modelo do usuário
            return View(usuario);
        }
    }
}
