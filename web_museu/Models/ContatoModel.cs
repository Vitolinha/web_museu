using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_museu.Models
{
    [Table("Contatos")] // Mapeia a classe para a tabela "Contatos" no banco de dados
    public class ContatoModel
    {
        [Key] // Define que este campo é a chave primária
        public int IdContato { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome pode ter no máximo 200 caracteres.")]
        [Column(TypeName = "nvarchar(200)")] // Define o tipo no banco como NVARCHAR(200)
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(200, ErrorMessage = "O e-mail pode ter no máximo 200 caracteres.")]
        [Column(TypeName = "nvarchar(200)")] // Define o tipo no banco como NVARCHAR(200)
        public string Email { get; set; }

        [Required(ErrorMessage = "O celular é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O celular pode ter no máximo 20 caracteres.")]
        [Column(TypeName = "nvarchar(20)")] // Define o tipo no banco como NVARCHAR(20)
        public string Celular { get; set; }

        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        [Column(TypeName = "datetime")] // Define o tipo no banco como DATETIME
        public DateTime DataCriacao { get; set; } = DateTime.Now; // Define um valor padrão válido
    }
}
