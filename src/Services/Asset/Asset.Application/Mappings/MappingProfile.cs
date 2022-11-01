namespace Asset.Application.Mappings
{
    using Asset.Application.Commands;
    using Asset.EventBus.Messages.Events;
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<CreateAssetLocationCommand, AssetCreateLocationEvent>().ReverseMap();
        }
    }
}
