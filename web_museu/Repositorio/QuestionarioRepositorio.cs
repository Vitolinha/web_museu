using web_museu.Data;
using web_museu.Models;

namespace web_museu.Repositorio
{
    public class QuestionarioRepositorio : IQuestionarioRepositorio
    {
        private readonly BancoContext _context;

        public QuestionarioRepositorio(BancoContext context)
        {
            _context = context;
        }

        public QuestionarioModel Adicionar(QuestionarioModel questionario)
        {
            _context.Questionarios.Add(questionario);
            _context.SaveChanges();
            return questionario;
        }
    }
}