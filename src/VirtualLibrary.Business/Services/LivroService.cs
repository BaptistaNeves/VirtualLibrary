using System;
using System.Linq;
using System.Threading.Tasks;
using VirtualLibrary.Business.Validators;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Interfaces.Services;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Business.Services
{
    public class LivroService : BaseService, ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        public LivroService(INotifier notifier, 
                            ILivroRepository livroRepository) : base(notifier)
        {
            _livroRepository = livroRepository;
        }

        public async Task Adicionar(Livro livro)
        {
            if(!ExecuteValidacao(new LivroValidation(), livro)) return;

            if(TituloJaExiste(livro.Titulo))
            {
                Notificar("Já existe um Livro cadastrado com o mesmo Titulo!");
                return;
            }

            await _livroRepository.Adicionar(livro);
        }

        public async Task Atualizar(Livro livro)
        {
             if(!ExecuteValidacao(new LivroValidation(), livro)) return;

            if(TituloJaExisteAtualizar(livro.Id, livro.Titulo))
            {
                Notificar("Já existe um Livro cadastrado com o mesmo Titulo!");
                return;
            }

            await _livroRepository.Atualizar(livro);
        }


        public async Task Remover(Livro livro)
        {
            if(TemAlugueres(livro.Id))
            {
                Notificar("Este livro Possui alugueres efectuados não pode ser Removido!");
                return;
            }

            await _livroRepository.Remover(livro);
        }

        public void Dispose()
        {
            _livroRepository?.Dispose();
        }

        private bool TituloJaExiste(string titulo) 
        {
            return _livroRepository.Buscar(c => c.Titulo == titulo).Result.Any();
        }

        private bool TituloJaExisteAtualizar(Guid id, string titulo)
        {
            return _livroRepository.Buscar(c => c.Titulo == titulo && c.Id != id)
                                    .Result.Any();
        }

        private bool TemAlugueres(Guid id)
        {
            return _livroRepository.ObterLivroAlugueresPorId(id).Result.Alugueres.Any();
        }
    }
}