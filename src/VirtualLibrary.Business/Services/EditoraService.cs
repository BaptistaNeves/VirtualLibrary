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
    public class EditoraService : BaseService, IEditoraService
    {
        private readonly IEditoraRepository _editoraRepository;
        public EditoraService(INotifier notifier, 
                            IEditoraRepository editoraRepository) : base(notifier)
        {
            _editoraRepository = editoraRepository;
        }

        public async Task Adicionar(Editora editora)
        {
            if(!ExecuteValidacao(new EditoraValidation(), editora)) return;

            if(EditoraExiste(editora.Nome))
            {
                Notificar("Uma Editora com o mesmo nome já foi cadastrada!");
                return;
            }

            await _editoraRepository.Adicionar(editora);
        }

        public async Task Atualizar(Editora editora)
        {
            if(!ExecuteValidacao(new EditoraValidation(),editora)) return;

            if(EditoraExisteAtualizar(editora.Id, editora.Nome))
            {
                Notificar("Uma Editora com o mesmo nome já foi cadastrada!");
                return;
            }

            await _editoraRepository.Atualizar(editora);
        }

        public async Task Remover(Editora editora)
        {
            if(TemLivros(editora.Id))
            {
                Notificar("Esta editora possui livros Cadastrados,não pode ser removida!");
                return;
            }

            await _editoraRepository.Remover(editora);
        }

        public void Dispose()
        {
            _editoraRepository?.Dispose();
        }

        private bool EditoraExiste(string nome)
        {
            return _editoraRepository.Buscar(e => e.Nome == nome).Result.Any();
        }

        private bool EditoraExisteAtualizar(Guid id, string nome)
        {
            return _editoraRepository   
                    .Buscar(e => e.Nome == nome && e.Id != id).Result.Any();
        }

        private bool TemLivros(Guid id)
        {
            return  _editoraRepository.ObterEditoraLivrosPorId(id)
                                    .Result.Livros.Any();
        }
    }
}