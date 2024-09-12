using AutoMapper;
using reservasAPI.DTOs;
using reservasAPI.Models;

namespace reservasAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Modelo -> DTO
            CreateMap<Cliente, ClienteResponse>();
            CreateMap<Mesa, MesaResponse>();
            CreateMap<Reserva, ReservaResponse>();

            //DTO -> Modelo 
            CreateMap<ClienteResquet, Cliente>();
            CreateMap<MesaResquet, Mesa>();
            CreateMap<ReservaResquet, Reserva>();

        }
    }
}
