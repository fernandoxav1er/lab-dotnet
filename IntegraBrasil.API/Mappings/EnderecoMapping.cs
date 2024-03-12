using AutoMapper;
using IntegraBrasil.API.Dtos;
using IntegraBrasil.API.Models;

namespace IntegraBrasil.API.Mappings
{
    public class EnderecoMapping : Profile
    {
        public EnderecoMapping()
        {
            CreateMap(typeof(ResponseGenerico<>),typeof(ResponseGenerico<>));
            CreateMap<EnderecoResponse, Endereco>();
            CreateMap<Endereco, EnderecoResponse>();
        }
    }
}
