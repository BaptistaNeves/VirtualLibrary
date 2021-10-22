using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualLibrary.Api.Extensions;
using VirtualLibrary.Domain.DTOs;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Interfaces.Services;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Api.Controllers
{
    [Authorize(Roles = "Moderador,Admin")]
    [Route("api/[controller]")]
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public ClientesController(INotifier notifier,
                                  IClienteRepository clienteRepository,
                                  IClienteService clienteService, 
                                  IMapper mapper) : base(notifier)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult> Obtertodos()
        {
            return Ok(_mapper.Map<IEnumerable<ClienteDto>>
                        (await _clienteRepository.ObterClientes()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            return Ok(_mapper.Map<ClienteDto>
                    (await _clienteRepository.ObterClientePorId(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(ClienteDto clienteDto)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            clienteDto.UsuarioId = User.ObterIdUsuario();
            clienteDto.DataCadastro = DateTime.Now;

            await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteDto));

            return CustomResponse(clienteDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, ClienteDto clienteDto)
        {
            var cliente = await _clienteRepository.ObterPorId(id);

            if(cliente == null)
            {
                NotificarErro("Cliente não encotrado!");
                return CustomResponse(clienteDto);
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            clienteDto.DataAtualizacao = DateTime.Now;
            clienteDto.UsuarioId = (Guid) cliente.UsuarioId;
            clienteDto.Id = cliente.Id;

            await _clienteService.Atualizar(_mapper.Map<Cliente>(clienteDto));

            return CustomResponse(clienteDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorId(id);

            if(cliente == null)
            {
                NotificarErro("Fornecedor não encotrado!");
                return CustomResponse(cliente);
            }

            await _clienteService.Remover(cliente);

            return CustomResponse(cliente);
        }
    }
}