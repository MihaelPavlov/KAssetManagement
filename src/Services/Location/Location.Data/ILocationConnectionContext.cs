namespace Location.Data
{
    using System.Data;

    public interface ILocationConnectionContext
    {
        string GetConnectionString();
        Task<IDbConnection> CreateConnection();
    }
}
