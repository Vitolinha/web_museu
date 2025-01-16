using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_museu.Models
{
    [Table("Questionarios")] // Mapeia a classe para a tabela "Questionarios" no banco de dados
    public class QuestionarioModel
    {
        [Key] // Define que este campo é a chave primária
        public int Id { get; set; }

        [Required] // Campo obrigatório
        [ForeignKey("Contato")] // Define a chave estrangeira
        public int ContatoId { get; set; }

        public ContatoModel Contato { get; set; } // Relacionamento com ContatoModel

        [Required(ErrorMessage = "A avaliação do design é obrigatória.")]
        [Range(1, 5, ErrorMessage = "A avaliação deve ser entre 1 e 5.")]
        public int AvaliacaoDesign { get; set; }

        [Required(ErrorMessage = "A avaliação da usabilidade é obrigatória.")]
        [Range(1, 5, ErrorMessage = "A avaliação deve ser entre 1 e 5.")]
        public int AvaliacaoUsabilidade { get; set; }

        [Required(ErrorMessage = "A avaliação do conteúdo é obrigatória.")]
        [Range(1, 5, ErrorMessage = "A avaliação deve ser entre 1 e 5.")]
        public int AvaliacaoConteudo { get; set; }

        public string Sugestao { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
