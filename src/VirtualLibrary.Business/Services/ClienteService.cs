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
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(INotifier notifier, 
                              IClienteRepository clienteRepository) : base(notifier)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task Adicionar(Cliente cliente)
        {
            if(!ExecuteValidacao(new ClienteValidation(), cliente)) return;

            if(EmailJaExiste(cliente.Email))
            {
                Notificar("Um cliente com o mesmo email já  foi cadastrado!");
                return;
            }

            await _clienteRepository.Adicionar(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            if(!ExecuteValidacao(new ClienteValidation(), cliente)) return;

            if(EmailExisteAtualizar(cliente.Id, cliente.Email))
            {
                Notificar("Um cliente com o mesmo email já  foi cadastrado!");
                return;
            }

            await _clienteRepository.Atualizar(cliente);
        }

        public async Task Remover(Cliente cliente)
        {
            if(TemAlugueres(cliente.Id))
            {
                Notificar("Este cliente possui alugueres efectuados,não pode ser removido!");
                return;
            }

            await _clienteRepository.Remover(cliente);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }

        private bool EmailJaExiste(string email)
        {
            return _clienteRepository.Buscar(c => c.Email == email).Result.Any();
        }

        private bool EmailExisteAtualizar(Guid id, string email)
        {
            return _clienteRepository.Buscar(c => c.Email == email && c.Id != id)
                                    .Result.Any();
        }

        private bool TemAlugueres(Guid id)
        {
            return _clienteRepository.ObterClienteAlugueresPorId(id)
                                    .Result.Alugueres.Any();
        }

    }
}