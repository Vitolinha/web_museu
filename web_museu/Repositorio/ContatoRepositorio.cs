using System;
using System.Collections.Generic;
using System.Linq;
using web_museu.Data;
using web_museu.Models;

namespace web_museu.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _dbContext;

        public ContatoRepositorio(BancoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ContatoModel> BuscarTodos()
        {
            return _dbContext.Contatos.ToList();
        }

        public ContatoModel BuscarPorId(int id)
        {
            return _dbContext.Contatos.FirstOrDefault(c => c.IdContato == id);
        }

        public void Adicionar(ContatoModel contato)
        {
            if (contato.DataCriacao == default)
            {
                contato.DataCriacao = DateTime.Now;
            }

            _dbContext.Contatos.Add(contato);
            _dbContext.SaveChanges();
        }

        public void Atualizar(ContatoModel contato)
        {
            // Busca o contato existente no banco de dados
            var contatoExistente = BuscarPorId(contato.IdContato);
            if (contatoExistente != null)
            {
                // Atualiza apenas os campos editáveis
                contatoExistente.Nome = contato.Nome;
                contatoExistente.Email = contato.Email;
                contatoExistente.Celular = contato.Celular;

                // Mantém a data de criação original
                contatoExistente.DataCriacao = contatoExistente.DataCriacao != default
                    ? contatoExistente.DataCriacao
                    : DateTime.Now;

                _dbContext.Contatos.Update(contatoExistente);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Contato não encontrado.");
            }
        }


        public bool Deletar(int id)
        {
            var contato = BuscarPorId(id);
            if (contato != null)
            {
                _dbContext.Contatos.Remove(contato);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
