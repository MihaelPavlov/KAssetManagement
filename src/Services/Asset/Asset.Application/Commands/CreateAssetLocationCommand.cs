namespace Asset.Application.Commands
{
    using MediatR;
    using MassTransit;
    using Asset.Application.Exceptions;
    using Asset.Application.Queries;
    using EventBus.Messages.AssetEvents;

    public class CreateAssetLocationCommand : IRequest
    {
        public int AssetId { get; set; }
        public int LocationId { get; set; }
        public DateTime CreationDate { get; set; }
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
            var assetById = await mediator.Send(new GetAssetByIdQuery(request.AssetId));

            if (assetById == null)
                throw new NotFoundException("Asset", request.AssetId);

            var eventMessage = new CreateAssetLocationEvent { AssetId = request.AssetId, LocationId = request.LocationId, UpdatedBy = 0/*userContext.UserId*/ };

            try
            {
                await publishEndpoint.Publish(eventMessage);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return Unit.Value;
        }
    }
}
