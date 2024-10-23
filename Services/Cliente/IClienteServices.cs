using reservasAPI.DTOs;

namespace reservasAPI.Services.Cliente
{
    public interface IClienteService
    {
        // Método para obtener todos los clientes
        Task<List<ClienteResponse>> GetClientes();

        // Método para obtener un cliente por su ID
        Task<ClienteResponse?> GetCliente(int id);

        // Método para crear un nuevo cliente
        Task<int> PostCliente(ClienteRequest cliente);

        // Método para actualizar un cliente existente
        Task<int> PutCliente(int id, ClienteRequest cliente);

        // Método para eliminar un cliente existente
        Task<int> DeleteCliente(int id);
    }
}
