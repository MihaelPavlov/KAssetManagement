using AutoMapper;
using EventBus.Messages.AssetEvents;
using Location.Data.Entities;
using Location.Service.Services.Interfaces;
using MassTransit;

namespace Location.API.IntegrationEvents.EventHandling
{
    public class CreateAssetLocationHandler : IConsumer<CreateAssetLocationEvent>
    {
        private readonly IMapper mapper;
        private readonly ILocationService locationService;

        public CreateAssetLocationHandler(IMapper mapper, ILocationService locationService)
        {
            this.mapper = mapper;
            this.locationService = locationService;
        }

        public async Task Consume(ConsumeContext<CreateAssetLocationEvent> context)
        {
            var model = this.mapper.Map<AssetLocation>(context.Message);

            await this.locationService.CreateAssetLocation(model);
        }
    }
}
