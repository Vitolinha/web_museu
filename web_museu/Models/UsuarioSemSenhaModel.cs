using System;
using System.ComponentModel.DataAnnotations;
using web_museu.Enums;

namespace web_museu.Models
{
    public class UsuarioSemSenhaModel
    {
        [Key] // Indica que esta é a chave primária
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário")]
        [MaxLength(200, ErrorMessage = "O nome pode ter no máximo 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        [MaxLength(100, ErrorMessage = "O login pode ter no máximo 100 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        [MaxLength(200, ErrorMessage = "O e-mail pode ter no máximo 200 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Selecione o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }

    }
}
