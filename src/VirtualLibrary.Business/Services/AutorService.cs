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
    public class AutorService : BaseService, IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        public AutorService(INotifier notifier, IAutorRepository autorRepository) : base(notifier)
        {
            _autorRepository = autorRepository;
        }

        public async Task Adicionar(Autor autor)
        {
            if(!ExecuteValidacao(new AutorValidation(), autor)) return;

            if(AutorJaExiste(autor.Nome)) {
                Notificar("Já existe um Autor com este Nome!");
                return;
            }

            await _autorRepository.Adicionar(autor);
        }

        public async Task Atualizar(Autor autor)
        {
            if(!ExecuteValidacao(new AutorValidation(), autor)) return;

            if(AutorJaExisteAtualizar(autor.Nome, autor.Id)) {
                Notificar("Já existe um Autor com este Nome!");
                return;
            }

            await _autorRepository.Atualizar(autor);
        }
        
        public async Task Remover(Autor autor)
        {
            if(PossuiLivros(autor.Id)) 
            {
                Notificar("Este autor possui livros, não pode ser removido!");
                return;
            }

            await _autorRepository.Remover(autor);
        }

        public bool AutorJaExiste(string nome)
        {
            return _autorRepository.Buscar(a => a.Nome == nome).Result.Any();
        }

        public bool AutorJaExisteAtualizar(string nome, Guid id)
        {
            return _autorRepository
                    .Buscar(a => a.Nome == nome && a.Id != id).Result.Any();
        }

        private bool PossuiLivros(Guid id)
        {
            return  _autorRepository
                    .ObterAutorLivrosPorId(id).Result.Livros.Any();
        }
        
        public void Dispose()
        {
            _autorRepository?.Dispose();
        }
    }
}