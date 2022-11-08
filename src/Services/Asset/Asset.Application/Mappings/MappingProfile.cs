namespace Asset.Application.Mappings
{
    using Asset.Application.Commands;
    using DAL = Asset.Domain.Entities;
    using AutoMapper;
    using Asset.Application.Queries;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<CreateAssetCommand, DAL.Asset>();
            this.CreateMap<UpdateAssetCommand, DAL.Asset>();
            this.CreateMap<GetAssetByIdQueryModel, DAL.Asset>();
        }
    }
}
