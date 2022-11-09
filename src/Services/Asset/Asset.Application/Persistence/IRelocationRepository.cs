namespace Asset.Application.Persistence
{
    using Asset.Domain.Entities;

    public interface IRelocationRepository : IAsyncRepository<RelocationRequest>
    {
    }
}
