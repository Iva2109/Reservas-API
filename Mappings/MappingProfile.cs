using AutoMapper;
using reservasAPI.DTOs;
using reservasAPI.Models;
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
            CreateMap<User, UserResponse>();
            

            //DTO -> Modelo 
            CreateMap<ClienteRequest, Cliente>();
            CreateMap<MesaResquet, Mesa>();
            CreateMap<ReservaRequest, Reserva>();
            CreateMap<UserRequest, User>();

        }
    }
}
