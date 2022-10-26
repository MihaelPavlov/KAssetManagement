namespace Asset.Application.Commands
{
    using MediatR;
    using AutoMapper;
    using MassTransit;
    using Asset.Application.Exceptions;
    using Asset.Application.Queries;
    using Asset.EventBus.Messages.Events;

    public class CreateAssetLocationCommand : IRequest
    {
        public int AssetId { get; set; }
        public int LocationId { get; set; }
        public DateTime dateTime { get; set; }
        public int? UpdatedBy { get; set; }
    }

    internal class CreateAssetLocationCommandHandler : IRequestHandler<CreateAssetLocationCommand>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IPublishEndpoint publishEndpoint;

        public CreateAssetLocationCommandHandler(IMapper mapper, IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(CreateAssetLocationCommand request, CancellationToken cancellationToken)
        {
            var assetById = await this.mediator.Send(new GetAssetByIdQuery(request.AssetId));

            if (assetById == null)
                throw new NotFoundException("Asset", request.AssetId);

            var eventMessage = this.mapper.Map<AssetCreateLocationEvent>(request);
            eventMessage.UpdatedBy = 0;

            await this.publishEndpoint.Publish(eventMessage);

            return Unit.Value;
        }
    }
}
