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
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fonecedorService;
        private readonly IMapper _mapper;
        public FornecedoresController(INotifier notifier,
                                      IFornecedorRepository forncedorRepository,
                                      IFornecedorService fonecedorService, 
                                      IMapper mapper) : base(notifier)
        {
            _fornecedorRepository = forncedorRepository;
            _fonecedorService = fonecedorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Obtertodos()
        {
            return Ok(_mapper.Map<IEnumerable<FornecedorDto>>
                        (await _fornecedorRepository.ObterFornecedores()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            return Ok(_mapper.Map<FornecedorDto>
                    (await _fornecedorRepository.ObterFornecedorPorId(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(FornecedorDto fornecedorDto)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            fornecedorDto.UsuarioId = User.ObterIdUsuario();
            fornecedorDto.DataCadastro = DateTime.Now;

            await _fonecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedorDto));

            return CustomResponse(fornecedorDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, FornecedorDto fornecedorDto)
        {
            var fornecedor = await _fornecedorRepository.ObterPorId(id);

            if(fornecedor == null)
            {
                NotificarErro("Fornecedor não encotrado!");
                return CustomResponse(fornecedorDto);
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            fornecedorDto.DataAtualizacao = DateTime.Now;
            fornecedorDto.Id = fornecedor.Id;
            fornecedorDto.UsuarioId = (Guid) fornecedor.UsuarioId;

            await _fonecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorDto));

            return CustomResponse(fornecedorDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterPorId(id);

            if(fornecedor == null)
            {
                NotificarErro("Fornecedor não encotrado!");
                return CustomResponse(fornecedor);
            }

            await _fonecedorService.Remover(fornecedor);

            return CustomResponse(fornecedor);
        }
    }
}