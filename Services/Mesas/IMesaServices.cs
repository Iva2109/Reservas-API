﻿namespace reservasAPI.Services.Mesas
{
    using global::reservasAPI.DTOs;
    

    namespace reservasAPI.Services.Mesa
    {
        public interface IMesaServices
        {
            Task<int> PostMesa(MesaResquet mesa);
            Task<List<MesaResponse>> GetMesas();
            Task<MesaResponse> GetMesa(int mesaId);
            Task<int> PutMesa(int mesaId, MesaResquet mesa);
            Task<int> DeleteMesa(int mesaId);
        }
    }

}