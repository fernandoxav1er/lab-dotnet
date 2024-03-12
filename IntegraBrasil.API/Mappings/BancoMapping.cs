using AutoMapper;
using IntegraBrasil.API.Dtos;
using IntegraBrasil.API.Models;

namespace IntegraBrasil.API.Mappings
{
    public class BancoMapping : Profile
    {
        public BancoMapping() 
        {
            CreateMap(typeof(ResponseGenerico<>), typeof(ResponseGenerico<>));
            CreateMap<BancoResponse, Banco>();
            CreateMap<Banco, BancoResponse>();
        }
    }
}
