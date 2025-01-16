using System.Collections.Generic;
using web_museu.Models;

namespace web_museu.Repositorio
{
    public interface IContatoRepositorio
    {
        IEnumerable<ContatoModel> BuscarTodos();
        ContatoModel BuscarPorId(int id); // Adicionar esta linha
        void Adicionar(ContatoModel contato);
        void Atualizar(ContatoModel contato);
        bool Deletar(int id);
    }
}
