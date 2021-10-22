using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualLibrary.Domain.DTOs;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Interfaces.Services;
using VirtualLibrary.Domain.Models;
using System.Security.Claims;
using VirtualLibrary.Api.Extensions;

namespace VirtualLibrary.Api.Controllers
{
    [Authorize(Roles ="Moderador,Admin")]
    [Route("api/[controller]")]
    public class CategoriasController : MainController
    {
        private readonly ICategoriaService _categoriaService;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository,
                                     ICategoriaService categoriaService,
                                     INotifier notifier, IMapper mapper) : base(notifier)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<CategoriaDto>>(await _categoriaRepository.ObterCategorias()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoriaDto>> ObterPorId(Guid id)
        {
            return Ok(_mapper.Map<CategoriaDto>(await _categoriaRepository.ObterCategoriaPorId(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(CategoriaDto categoria)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            categoria.DataCadastro = DateTime.Now;
            
            categoria.UsuarioId = User.ObterIdUsuario();

            await _categoriaService.Adicionar(_mapper.Map<Categoria>(categoria));

            return CustomResponse(categoria);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, CategoriaDto categoriaDto)
        {
            var categoriaAtualizada = await _categoriaRepository.ObterPorId(id);

            if(categoriaAtualizada == null)
            {
                NotificarErro("O código do produto informado não foi encontrado!");
                return CustomResponse(categoriaDto);
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            categoriaAtualizada.DataAtualizacao = DateTime.Now;
            categoriaAtualizada.Descricao = categoriaDto.Descricao;

            await _categoriaService.Atualizar(categoriaAtualizada);

            return CustomResponse(categoriaDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var categoria = await _categoriaRepository.ObterPorId(id);

            if(categoria == null)
            {
                NotificarErro("O código do produto informado não foi encontrado!");
                return CustomResponse(categoria);
            }

            await _categoriaService.Remover(categoria);
            
            return CustomResponse(categoria);
        }

    }
}