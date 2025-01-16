using web_museu.Models;

namespace web_museu.Repositorio
{
    public interface IQuestionarioRepositorio
    {
        QuestionarioModel Adicionar(QuestionarioModel questionario);
    }
}
