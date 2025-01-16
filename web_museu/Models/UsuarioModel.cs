using System;
using System.ComponentModel.DataAnnotations;
using web_museu.Enums;
using web_museu.Helper;

namespace web_museu.Models
{
    public class UsuarioModel
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

        [Required(ErrorMessage = "Digite a senha do usuário")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A data de cadastro é obrigatória")]
        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Atualização")]
        [DataType(DataType.DateTime)]
        public DateTime? DataAtualizacao { get; set; }
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            // Gera uma nova senha baseada em um GUID e limita a 8 caracteres
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);

            // Retorna a nova senha gerada
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }

    }
}
