using Asset.Application.Interfaces;
using Asset.Application.Persistence;
using Asset.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Asset.Application.Queries.Renovation
{
    public class GetRenovationRequestListQuery : IRequest<List<GetRenovationRequestQueryModel>>
    {
    }

    internal class GetRenovationRequestListQueryHandler : IRequestHandler<GetRenovationRequestListQuery, List<GetRenovationRequestQueryModel>>
    {
        private readonly IMapper mapper;
        private readonly IRenovationRepository renovationRepository;

        public GetRenovationRequestListQueryHandler(IRenovationRepository renovationRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.renovationRepository = renovationRepository;
        }

        public async Task<List<GetRenovationRequestQueryModel>> Handle(GetRenovationRequestListQuery request, CancellationToken cancellationToken)
        {
            var result = await this.renovationRepository.GetAllAsync();

            return this.mapper.Map<List<GetRenovationRequestQueryModel>>(result);
        }
    }

    public class GetRenovationRequestQueryModel : IMapFrom<RenovationRequest>
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
