namespace Location.API.EventBusConsumer
{
    using Asset.EventBus.Messages.Events;
    using AutoMapper;
    using Location.Data.Entities;
    using Location.Service.Services.Interfaces;
    using MassTransit;

    public class CreateAssetLocationConsumer : IConsumer<AssetCreateLocationEvent>
    {
        private readonly IMapper mapper;
        private readonly ILocationService locationService;

        public CreateAssetLocationConsumer(IMapper mapper, ILocationService locationService)
        {
            this.mapper = mapper;
            this.locationService = locationService;
        }

        public async Task Consume(ConsumeContext<AssetCreateLocationEvent> context)
        {
            var model = this.mapper.Map<AssetLocation>(context.Message);

            await this.locationService.CreateAssetLocation(model);
        }
    }
}
