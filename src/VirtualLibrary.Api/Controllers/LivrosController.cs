using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualLibrary.Api.Extensions;
using VirtualLibrary.Domain.DTOs;
using VirtualLibrary.Domain.Interfaces.Notifications;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Interfaces.Services;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Api.Controllers
{
    [Authorize(Roles="Moderador,Admin")]
    [Route("api/[controller]")]
    public class LivrosController : MainController
    {
        private readonly ILivroRepository _livrosRepository;
        private readonly ILivroService _livroService;
        private readonly IMapper _mapper;
        public LivrosController(INotifier notifier, 
                                ILivroRepository livrosRepository, 
                                ILivroService livroService, 
                                IMapper mapper) : base(notifier)
        {
           _livrosRepository = livrosRepository;
           _livroService = livroService;
           _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ObterTodos()
        {
            return Ok(_mapper.Map<IEnumerable<LivroDto>>(await _livrosRepository.ObterDetalhesDeLivros()));
        }

        [HttpGet("livros-por-categoria/{id:guid}")]
        public async Task<ActionResult> ObterPorCategoriaId(Guid id)
        {
            return Ok(_mapper.Map<IEnumerable<LivroDto>>(await _livrosRepository.ObterLivroPorCategoriaId(id)));
        }
 

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {
            return Ok(_mapper.Map<LivroDto>(await _livrosRepository.ObterDetalhesDeLivroPorId(id)));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(LivroDto livroDto)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var imgPrefixo = Guid.NewGuid() + "_";

            if(!await UploadImagem(livroDto.ImagemLivro, imgPrefixo))
            {
                return CustomResponse(livroDto);
            }

            livroDto.DataCadastro = DateTime.Now;
            livroDto.UsuarioId = User.ObterIdUsuario();
            livroDto.Imagem = imgPrefixo + livroDto.ImagemLivro.FileName;

            await _livroService.Adicionar(_mapper.Map<Livro>(livroDto));

            return CustomResponse(livroDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, LivroAtualizarDto livroDto)
        {
            var livro = await _livrosRepository.ObterPorId(id);

            if(livro == null)
            {
                NotificarErro("O livro selecionado não foi encontrado!");
                return CustomResponse(livroDto);
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            if(livroDto.ImagemLivro == null)
            {
                livroDto.Imagem = livro.Imagem;
            } 
            else
            {
                RemoverImagem(livro.Imagem);

                var imgPrefixo = Guid.NewGuid() + "_";

                if(!await UploadImagem(livroDto.ImagemLivro, imgPrefixo))
                {
                    return CustomResponse(livroDto);
                }

                livroDto.Imagem = imgPrefixo + livroDto.ImagemLivro.FileName; 
            }         

            livroDto.DataAtualizacao = DateTime.Now;
            livroDto.Id = livro.Id;

            await _livroService.Atualizar(_mapper.Map<Livro>(livroDto));

            return CustomResponse(livroDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var livro = await _livrosRepository.ObterPorId(id);

            if(livro == null)
            {
                return CustomResponse("O Livro seleciinado não foi encontrado!");
            }

            await _livroService.Remover(livro);

            return CustomResponse();
        }

        [NonAction]
        public async Task<bool> UploadImagem(IFormFile arquivo, string imgPrefixo)
        {
            if(arquivo == null || arquivo.Length == 0)
            {
                NotificarErro("Forneça uma Imagem para este Livro!");
                return false;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

            if(System.IO.File.Exists(path))
            {
                NotificarErro("Já existe um Arquivo com Este Nome!");
                return false;
            }

            using(var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream); //Cópia o conteúdo do arquivo enviado para o caminho apontado pelo stream
            }

            return true;
        }

        [NonAction]
        public void RemoverImagem(string arquivo)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + arquivo);

            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            NotificarErro("Arquivo Imagem não encontrado!");
        }
    }
}