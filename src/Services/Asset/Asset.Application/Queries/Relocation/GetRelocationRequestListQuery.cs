namespace Asset.Application.Queries.Relocation
{
    using MediatR;
    using Asset.Domain.Entities;
    using Asset.Application.Interfaces;
    using System.Threading.Tasks;
    using System.Threading;
    using Asset.Application.Persistence;
    using AutoMapper;

    public class GetRelocationRequestListQuery : IRequest<List<GetRelocationRequestQueryModel>>
    {
    }

    internal class GetRelocationRequestListQueryHandler : IRequestHandler<GetRelocationRequestListQuery, List<GetRelocationRequestQueryModel>>
    {
        private readonly IMapper mapper;
        private readonly IRelocationRepository relocationRepository;

        public GetRelocationRequestListQueryHandler(IRelocationRepository relocationRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.relocationRepository = relocationRepository;
        }

        public async Task<List<GetRelocationRequestQueryModel>> Handle(GetRelocationRequestListQuery request, CancellationToken cancellationToken)
        {
            var result = await relocationRepository.GetAllAsync();

            return mapper.Map<List<GetRelocationRequestQueryModel>>(result);
        }
    }

    public class GetRelocationRequestQueryModel : IMapFrom<RelocationRequest>
    {
        public int AssetId { get; set; }

        public int FromSiteId { get; set; }
        public int ToSiteId { get; set; }

        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }

        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }

        public int Status { get; set; } // RequestStatus
        public int GetRequest { get; set; } // RequestStatus
        public int Received { get; set; } // RequestStatus

        // TODO: add createdDate
    }
}
