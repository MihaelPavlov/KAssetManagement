namespace Asset.Application.Queries.Relocation
{
    using MediatR;
    using Asset.Application.Interfaces;
    using DAL = Domain.Entities;
    using Asset.Application.Persistence;
    using AutoMapper;

    public class GetRelocationRequestByIdQuery : IRequest<GetRelocationRequestByIdQueryModel>
    {
        public int RequestId { get; set; }

        public GetRelocationRequestByIdQuery(int requestId)
        {
            RequestId = requestId;
        }
    }

    internal class GetRelocationRequestQueryHandler : IRequestHandler<GetRelocationRequestByIdQuery, GetRelocationRequestByIdQueryModel>
    {
        private readonly IMapper mapper;
        private readonly IRelocationRepository relocationRepository;

        public GetRelocationRequestQueryHandler(IRelocationRepository relocationRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.relocationRepository = relocationRepository;
        }

        public async Task<GetRelocationRequestByIdQueryModel> Handle(GetRelocationRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await relocationRepository.GetByIdAsync(request.RequestId);

            return mapper.Map<GetRelocationRequestByIdQueryModel>(result);
        }
    }

    public class GetRelocationRequestByIdQueryModel : IMapFrom<DAL.RelocationRequest>
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
    }
}
