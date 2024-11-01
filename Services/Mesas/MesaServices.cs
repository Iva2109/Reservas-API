using AutoMapper;
using Microsoft.EntityFrameworkCore;
using reservasAPI.DTOs;
using reservasAPI.Models;
using reservasAPI.Services.Mesas.reservasAPI.Services.Mesa;

namespace reservasAPI.Services.Mesa
{
    public class MesaServices : IMesaServices
    {
        private readonly ReservasdbContext _db;
        private readonly IMapper _mapper;

        public MesaServices(ReservasdbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> DeleteMesa(int mesaId)
        {
            var mesa = await _db.Mesas.FindAsync(mesaId);
            if (mesa == null)
                return -1;

            _db.Mesas.Remove(mesa);
            return await _db.SaveChangesAsync();
        }

        public async Task<MesaResponse> GetMesa(int mesaId)
        {
            var mesa = await _db.Mesas.FindAsync(mesaId);
            var mesaResponse = _mapper.Map<reservasAPI.Models.Mesa, MesaResponse>(mesa);
            return mesaResponse;
        }

        public async Task<List<MesaResponse>> GetMesas()
        {
            var mesas = await _db.Mesas.ToListAsync();
            var mesasList = _mapper.Map<List<reservasAPI.Models.Mesa>, List<MesaResponse>>(mesas);
            return mesasList;
        }

        public async Task<int> PostMesa(MesaRequest mesa)
        {
            // Verificar si ya existe una mesa con la misma Capacidad y Ubicacion
            var mesaDuplicada = await _db.Mesas
                .FirstOrDefaultAsync(m => m.Capacidad == mesa.Capacidad && m.Ubicacion == mesa.Ubicacion);

            if (mesaDuplicada != null)
            {
                // Opcionalmente, puedes lanzar una excepción aquí o devolver un valor especial
                throw new InvalidOperationException("Ya existe una mesa con la misma capacidad y ubicación.");
            }

            // Mapear y agregar la nueva mesa si no hay duplicados
            var mesaRequest = _mapper.Map<MesaRequest, reservasAPI.Models.Mesa>(mesa);
            await _db.Mesas.AddAsync(mesaRequest);
            await _db.SaveChangesAsync();
            return mesaRequest.IdMesa;
        }

        public async Task<int> PutMesa(int mesaId, MesaRequest mesa)
        {
            var entity = await _db.Mesas.FindAsync(mesaId);
            if (entity == null)
                return -1;

            entity.Capacidad = mesa.Capacidad;
            entity.Ubicacion = mesa.Ubicacion;
            entity.Estado = mesa.Estado;
            entity.UserId = mesa.UserId;

            _db.Mesas.Update(entity);

            return await _db.SaveChangesAsync();
        }
    }
}
