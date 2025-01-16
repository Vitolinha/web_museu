using System.ComponentModel.DataAnnotations;

namespace web_museu.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string Senha { get; set; }
    }
}
