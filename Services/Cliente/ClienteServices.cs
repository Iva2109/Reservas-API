using AutoMapper;
using Microsoft.EntityFrameworkCore;
using reservasAPI.DTOs;
using reservasAPI.Models;

namespace reservasAPI.Services.Cliente
{
    public class ClienteService : IClienteService
    {
        private readonly ReservasdbContext _db;
        private readonly IMapper _mapper;

        public ClienteService(ReservasdbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Método para eliminar un cliente por ID
        public async Task<int> DeleteCliente(int clienteId)
        {
            var cliente = await _db.Clientes.FindAsync(clienteId);
            if (cliente == null)
                return -1;

            _db.Clientes.Remove(cliente);
            return await _db.SaveChangesAsync();
        }

        // Método para obtener un cliente por ID
        public async Task<ClienteResponse> GetCliente(int clienteId)
        {
            var cliente = await _db.Clientes.FindAsync(clienteId);
            if (cliente == null)
                return null!;

            var clienteResponse = _mapper.Map<reservasAPI.Models.Cliente, ClienteResponse>(cliente);
            return clienteResponse;
        }

        // Método para obtener la lista de todos los clientes
        public async Task<List<ClienteResponse>> GetClientes()
        {
            var clientes = await _db.Clientes.ToListAsync();
            var clientesList = _mapper.Map<List<Models.Cliente>, List<ClienteResponse>>(clientes);
            return clientesList;
        }

        // Método para crear un nuevo cliente
        public async Task<int> PostCliente(ClienteRequest cliente)
        {
            var clienteEntity = _mapper.Map<ClienteRequest, reservasAPI.Models.Cliente>(cliente);
            await _db.Clientes.AddAsync(clienteEntity);
            await _db.SaveChangesAsync();
            return clienteEntity.IdCliente; // Devuelve el ID del cliente recién creado
        }

        // Método para actualizar un cliente existente
        public async Task<int> PutCliente(int clienteId, ClienteRequest cliente)
        {
            var entity = await _db.Clientes.FindAsync(clienteId);
            if (entity == null)
                return -1;

            entity.Nombre = cliente.Nombre;
            entity.Correo = cliente.Correo;
            entity.Telefono = cliente.Telefono;
            entity.FechaRegistro = cliente.FechaRegistro;

            _db.Clientes.Update(entity);
            return await _db.SaveChangesAsync();
        }
    }
}
