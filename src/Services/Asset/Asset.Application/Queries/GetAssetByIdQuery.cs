using Asset.Application.Exceptions;
using Asset.Application.Interfaces;
using Asset.Application.Persistence;
using Asset.Domain.Enums;
using AutoMapper;
using MediatR;
using DAL = Asset.Domain.Entities;

namespace Asset.Application.Queries
{
    public class GetAssetByIdQuery : IRequest<GetAssetByIdQueryModel>
    {
        public int AssetId { get; set; }

        public GetAssetByIdQuery(int assetId)
        {
            this.AssetId = assetId;
        }
    }

    internal class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, GetAssetByIdQueryModel>
    {
        private readonly IMapper mapper;
        private readonly IAssetRepository assetRepository;

        public GetAssetByIdQueryHandler(IMapper mapper, IAssetRepository assetRepository)
        {
            this.mapper = mapper;
            this.assetRepository = assetRepository;
        }

        public async Task<GetAssetByIdQueryModel> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await this.assetRepository.GetByIdAsync(request.AssetId);

            if (result == null)
                throw new NotFoundException(nameof(request.AssetId), "Asset not found!");

            return this.mapper.Map<GetAssetByIdQueryModel>(result);
        }
    }

    #region models
    public class GetAssetByIdQueryModel : IMapFrom<DAL.Asset>
    {
        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        public int GuarantyMounts { get; set; }
        public int LocationId { get; set; }
        public string? Producer { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Price { get; set; }
        public AssetType Type { get; set; }
        public AssetPeriodType PeriodType { get; set; }
        public AssetStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DAL.Asset, GetAssetByIdQueryModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(x => (AssetType)x.Type))
                .ForMember(dest => dest.PeriodType, opt => opt.MapFrom(x => (AssetPeriodType)x.PeriodType))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => (AssetStatus)x.Status));
        }
    }
    #endregion models
}
