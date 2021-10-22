using System;
using System.Linq;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Interfaces.Services;
using VirtualLibrary.Domain.Models;
using VirtualLibrary.Business.Validators;

namespace VirtualLibrary.Business.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private readonly  ICategoriaRepository _categoriaRepository;
        public CategoriaService(INotifier notifier, ICategoriaRepository categoriaRepository) : base(notifier)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Adicionar(Categoria categoria)
        {
            if(!ExecuteValidacao(new CategoriaValidation(), categoria)) return;

            if(DescricaoJaExiste(categoria.Descricao)) 
            {
                Notificar("Já existe uma Descrição com este Nome!");
                return;
            }

            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task Atualizar(Categoria categoria)
        {
            if(!ExecuteValidacao(new CategoriaValidation(), categoria)) return;

            if(DescricaoJaExisteAtualizar(categoria.Descricao, categoria.Id))
            {
                Notificar("Já existe uma Descrição com este Nome!");
                return;
            }

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task Remover(Categoria categoria)
        {
            if(TemLivros(categoria.Id)) {
                Notificar("O Forncedor possui Livros cadastrados");
                return;
            }

            await _categoriaRepository.Remover(categoria);
        }

        private bool DescricaoJaExiste(string descricao)
        {
            return _categoriaRepository.Buscar(c => c.Descricao == descricao).Result.Any();
        }

        private bool DescricaoJaExisteAtualizar(string descricao, Guid id)
        {
            return _categoriaRepository.Buscar(c => c.Descricao == descricao && c.Id != id)
                                        .Result.Any();
        }

        private bool TemLivros(Guid id)
        {
            return _categoriaRepository.ObterCategoriaLivrosPorId(id).Result.Livros.Any();
        }
    }
}