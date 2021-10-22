using System.Linq;
using AutoMapper;
using VirtualLibrary.Api.Extensions;
using VirtualLibrary.Domain.DTOs;
using VirtualLibrary.Domain.DTOs.Usuario;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Api.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Editora, EditoraDto>().ReverseMap();

            CreateMap<Fornecedor, FornecedorDto>()
            .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => 
            src.DataNascimento.CalculateAge()));

            CreateMap<FornecedorDto, Fornecedor>();
            
            CreateMap<Cliente, ClienteDto>()
               .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => 
                src.DataNascimento.CalculateAge()));
                
            CreateMap<ClienteDto, Cliente>();

            CreateMap<Autor, AutorDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}