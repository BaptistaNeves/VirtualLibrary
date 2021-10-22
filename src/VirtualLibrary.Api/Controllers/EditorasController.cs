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
    public class EditorasController : MainController
    {
        private readonly IEditoraRepository _editoraRepository;
        private readonly IEditoraService _editoraService;
        private readonly IMapper _mapper;
        public EditorasController(INotifier notifier,
                                  IEditoraRepository editoraRepository,
                                  IEditoraService editoraService, 
                                  IMapper mapper) : base(notifier)
        {
            _editoraRepository = editoraRepository;
            _editoraService = editoraService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<EditoraDto>>(await _editoraRepository.ObterEditoras()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            return Ok(_mapper.Map<EditoraDto>(await _editoraRepository.ObterEditoraPorId(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(EditoraDto editoraDto)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            editoraDto.DataCadastro = DateTime.Now;
            editoraDto.UsuarioId = User.ObterIdUsuario();

            await _editoraService.Adicionar(_mapper.Map<Editora>(editoraDto));

            return CustomResponse(editoraDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, EditoraDto editoraDto)
        {
            var editora = await _editoraRepository.ObterPorId(id);

            if(editora == null)
            {
                NotificarErro("Esta Editora não foi encontrada!");
                return CustomResponse(editoraDto);
            }

            editoraDto.DataAtualizacao = DateTime.Now;
            editoraDto.UsuarioId = User.ObterIdUsuario();
            editoraDto.Id = editora.Id;

            await _editoraService.Atualizar(_mapper.Map<Editora>(editoraDto));

            return CustomResponse(editoraDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var editora = await _editoraRepository.ObterPorId(id);

            if(editora == null)
            {
                NotificarErro("Esta Editora não foi encontrada!");
                return CustomResponse(editora);
            }

            await _editoraService.Remover(editora);

            return CustomResponse(editora);
        }
    }
}