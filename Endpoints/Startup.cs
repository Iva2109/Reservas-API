namespace reservasAPI.Endpoints
{
    public  static class Startup
    {
        public static void  UseEndpoints(this WebApplication app) {
            ReservaEdpoints.Add(app);
            UserEndpoints.Add(app);
        }
    }
}
