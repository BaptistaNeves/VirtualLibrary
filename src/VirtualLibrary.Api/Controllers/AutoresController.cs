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
    [Authorize(Roles ="Moderador,Admin")]
    [Route("api/[controller]")]
    public class AutoresController : MainController
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IAutorService _autorService;
        private readonly IMapper _mapper;
        public AutoresController(INotifier notifier,
                                 IAutorRepository autorRepository,
                                 IAutorService autorService, 
                                 IMapper mapper) : base(notifier)
        {
            _autorRepository = autorRepository;
            _autorService = autorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDto>>> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<AutorDto>>(await _autorRepository.ObterAutores()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AutorDto>> ObterPorId(Guid id)
        {
            return Ok(_mapper.Map<AutorDto>(await _autorRepository.ObterAutorPorId(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(AutorDto autor)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            autor.DataCadastro = DateTime.Now;
            autor.UsuarioId = User.ObterIdUsuario();

            await _autorService.Adicionar(_mapper.Map<Autor>(autor));

            return CustomResponse(autor);
            
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, AutorDto autorDto)
        {
            // var autor = await _autorRepository.ObterAutorPorId(id);

            // if(autor == null)
            // {
            //     NotificarErro("Autor não é encontrado!");
            //     return CustomResponse(autorDto);
            // }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            autorDto.DataAtualizacao = DateTime.Now;
            autorDto.UsuarioId = User.ObterIdUsuario();
            autorDto.Id = id;

            await _autorService.Atualizar(_mapper.Map<Autor>(autorDto));

            return CustomResponse(autorDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var autor = await _autorRepository.ObterPorId(id);

            if(autor == null)
            {
                NotificarErro("Autor não é encontrado!");
                return CustomResponse();
            }

            await _autorService.Remover(autor);

            return CustomResponse();
        }
    }
}