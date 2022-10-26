using Asset.Application.Commands;
using Asset.Application.Queries;
using Asset.EventBus.Messages.Events;
using AutoMapper;

namespace Asset.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<CreateAssetLocationCommand, AssetCreateLocationEvent>().ReverseMap();
        }
    }
}
