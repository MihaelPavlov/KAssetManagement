namespace Asset.Application.Persistence
{
    using Asset.Domain.Entities;

    public interface IRenovationRepository : IAsyncRepository<RenovationRequest>
    {
    }
}
