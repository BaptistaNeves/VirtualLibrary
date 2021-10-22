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
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        public FornecedorService(INotifier notifier, 
                                IFornecedorRepository fornecedorRepository) : base(notifier)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            if(!ExecuteValidacao(new FornecedorValidation(),fornecedor)) return;

            if(FornecedorJaExiste(fornecedor.Nome))
            {
                Notificar("Um Fornecedor com o mesmo nome já foi cadastrado!");
                return;
            }

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
             if(!ExecuteValidacao(new FornecedorValidation(),fornecedor)) return;

            if(FornecedExisteAtualizar(fornecedor.Id, fornecedor.Nome))
            {
                Notificar("Um Fornecedor com o mesmo nome já foi cadastrado!");
                return;
            }

            await _fornecedorRepository.Atualizar(fornecedor);
        }
       
        public async Task Remover(Fornecedor fornecedor)
        {
            if(TemLivros(fornecedor.Id))
            {
                Notificar("Este fornecedor possui livros cadastrados,não pode ser removido!");
                return;
            }

            await _fornecedorRepository.Remover(fornecedor);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
        }

        private bool FornecedorJaExiste(string nome)
        {
            return _fornecedorRepository.Buscar(f => f.Nome == nome).Result.Any();
        }

        private bool FornecedExisteAtualizar(Guid id, string nome)
        {
            return _fornecedorRepository.Buscar(f => f.Nome == nome && f.Id != id)
                                        .Result.Any();
        }

        private bool TemLivros(Guid id)
        {
            return _fornecedorRepository.ObterFornecedorLivrosPorId(id)
                                        .Result.Livros.Any();
        }
    }
}