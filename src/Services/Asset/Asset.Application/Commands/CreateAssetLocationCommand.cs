namespace Asset.Application.Commands
{
    using MediatR;
    using MassTransit;
    using Asset.Application.Exceptions;
    using Asset.Application.Queries;
    using Events = Asset.EventBus.Messages.Events;

    public class CreateAssetLocationCommand : IRequest
    {
        public int AssetId { get; set; }
        public int LocationId { get; set; }
        public DateTime dateTime { get; set; }
    }

    internal class CreateAssetLocationCommandHandler : IRequestHandler<CreateAssetLocationCommand>
    {
        private readonly IMediator mediator;
        private readonly IPublishEndpoint publishEndpoint;

        public CreateAssetLocationCommandHandler(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            this.mediator = mediator;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(CreateAssetLocationCommand request, CancellationToken cancellationToken)
        {
            var assetById = await this.mediator.Send(new GetAssetByIdQuery(request.AssetId));

            if (assetById == null)
                throw new NotFoundException("Asset", request.AssetId);

            var eventMessage = new Events.AssetCreateLocationEvent(request.AssetId, request.LocationId, /*userContext.UserId*/ 0);

            await this.publishEndpoint.Publish(eventMessage);

            return Unit.Value;
        }
    }
}
