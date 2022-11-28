namespace Asset.Application.Mappings
{
    using Asset.Application.Commands;
    using DAL = Asset.Domain.Entities;
    using AutoMapper;
    using Asset.Application.Queries;
    using Asset.Application.Commands.Relocation;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Asset
            this.CreateMap<CreateAssetCommand, DAL.Asset>();
            this.CreateMap<UpdateAssetCommand, DAL.Asset>();
            this.CreateMap<GetAssetByIdQueryModel, DAL.Asset>();

            // Relocation
            this.CreateMap<CreateRelocationRequestCommand, DAL.RelocationRequest>();
        }
    }
}
