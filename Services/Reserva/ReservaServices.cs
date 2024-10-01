using AutoMapper;
using Microsoft.EntityFrameworkCore;
using reservasAPI.DTOs;
using reservasAPI.Models;
using System.Net.Http.Headers;

namespace reservasAPI.Services.Reserva
{
    public class ReservaServices :IReservaServices
    {
        private readonly ReservasdbContext _db;
        private readonly IMapper _mapper;

        public ReservaServices(ReservasdbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> DeleteReserva(int reservaId)
        {
            var reserva = await  _db.Reservas.FindAsync(reservaId);
            if (reserva == null)
                return -1;

            _db.Reservas.Remove(reserva);
            return await _db.SaveChangesAsync();
        }

        public async Task<ReservaResponse> GetReserva(int reservaId)
        {
            var reserva = await _db.Reservas.FindAsync(reservaId);
            var reservaResponse = _mapper.Map<reservasAPI.Models.Reserva, ReservaResponse>(reserva);
            return reservaResponse;
        }

        public async Task<List<ReservaResponse>> GetReservas()
        {
            var reservas = await _db.Reservas.ToListAsync();
            var reservasList = _mapper.Map<List<reservasAPI.Models.Reserva>, List<ReservaResponse>>(reservas);
            return reservasList;
        }

        public async Task<int> PostReserva(ReservaRequest reserva)
        {
            var reservaResquet = _mapper.Map<ReservaRequest, reservasAPI.Models.Reserva>(reserva);
            await _db.Reservas.AddAsync(reservaResquet);
            await _db.SaveChangesAsync();
            return reservaResquet.IdReserva;
        }

        public async Task<int> PutReserva(int reservaId, ReservaRequest reserva)
        {
           var entity = await _db.Reservas.FindAsync(reservaId);
           if (entity == null)
                return -1;

            entity.FechaReserva = reserva.FechaReserva;
            entity.HoraReserva = reserva.HoraReserva;
            entity.NumPersonas = reserva.NumPersonas;
            entity.Estado = reserva.Estado;
            entity.UserId = reserva.UserId;
            entity.IdCliente = reserva.IdCliente;
            entity.IdMesa = reserva.IdMesa;

            _db.Reservas.Update(entity);

            return await _db.SaveChangesAsync();

        }
    }
}

