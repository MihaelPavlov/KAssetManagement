namespace Asset.Application.Queries.Renovation
{
    using Asset.Application.Interfaces;
    using Asset.Application.Persistence;
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using DAL = Domain.Entities;

    public class GetRenovationRequestByIdQuery : IRequest<GetRenovationRequestByIdQueryModel>
    {
        public int RequestId { get; set; }

        public GetRenovationRequestByIdQuery(int requestId)
        {
            RequestId = requestId;
        }
    }

    internal class GetRenovationRequestByIdQueryHandler : IRequestHandler<GetRenovationRequestByIdQuery, GetRenovationRequestByIdQueryModel>
    {
        private readonly IMapper mapper;
        private readonly IRenovationRepository renovationRepository;

        public GetRenovationRequestByIdQueryHandler(IRenovationRepository renovationRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.renovationRepository = renovationRepository;
        }

        public async Task<GetRenovationRequestByIdQueryModel> Handle(GetRenovationRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await this.renovationRepository.GetByIdAsync(request.RequestId);

            return this.mapper.Map<GetRenovationRequestByIdQueryModel>(result);
        }
    }

    public class GetRenovationRequestByIdQueryModel : IMapFrom<DAL.RenovationRequest>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProblemMessage { get; set; } = string.Empty;
        public int Status { get; set; }
        public int IsItGiven { get; set; }
        public int IsItRenovated { get; set; }
    }
}
