using reservasAPI.DTOs;

namespace reservasAPI.Services.Reserva
{
    public interface IReservaServices
    {
        Task<int> PostReserva(ReservaRequest reserva);
        Task<List<ReservaResponse>> GetReservas();
        Task<ReservaResponse> GetReserva(int reservaId);
        Task<int> PutReserva(int reservaId, ReservaRequest reserva);
        Task<int> DeleteReserva(int reservaId);
   

    }
}
