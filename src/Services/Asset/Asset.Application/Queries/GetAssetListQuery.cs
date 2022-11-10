namespace Asset.Application.Queries
{
    using Asset.Application.Interfaces;
    using Asset.Application.Persistence;
    using Asset.Domain.Entities;
    using Asset.Domain.Enums;
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAssetListQuery : IRequest<List<GetAssetListQueryModel>>
    {
    }

    internal class GetAssetListQueryHandler : IRequestHandler<GetAssetListQuery, List<GetAssetListQueryModel>>
    {
        private readonly IAssetRepository assetRepository;
        private readonly IMapper mapper;

        public GetAssetListQueryHandler(IAssetRepository assetRepository, IMapper mapper)
        {
            this.assetRepository = assetRepository ?? throw new ArgumentNullException(nameof(assetRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<GetAssetListQueryModel>> Handle(GetAssetListQuery request, CancellationToken cancellationToken)
        {
            var result = await this.assetRepository.GetAllAsync();
            return this.mapper.Map<List<GetAssetListQueryModel>>(result);
        }
    }

    #region models
    public class GetAssetListQueryModel : IMapFrom<Asset>
    {
        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        public int GuarantyMounts { get; set; }
        public int LocationId { get; set; }
        public string? Producer { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public decimal? Price { get; set; }
        public AssetType Type { get; set; }
        public AssetPeriodType PeriodType { get; set; }
        public AssetStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Asset, GetAssetListQueryModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(x => (AssetType)x.Type))
                .ForMember(dest => dest.PeriodType, opt => opt.MapFrom(x => (AssetPeriodType)x.PeriodType))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => (AssetStatus)x.Status));
        }
    }
    #endregion models
}
