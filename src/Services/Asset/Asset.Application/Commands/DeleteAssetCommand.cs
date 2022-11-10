namespace Asset.Application.Commands
{
    using Asset.Application.Exceptions;
    using Asset.Application.Persistence;
    using Asset.Application.Queries;
    using AutoMapper;
    using MediatR;
    using DAL = Asset.Domain.Entities;

    public class DeleteAssetCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteAssetCommand(int id)
        {
            this.Id = id;
        }
    }

    internal class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IAssetRepository assetRepository;

        public DeleteAssetCommandHandler(IAssetRepository assetRepository, IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.assetRepository = assetRepository;
        }
        public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await this.mediator.Send(new GetAssetByIdQuery(request.Id));

            if (asset == null)
                throw new NotFoundException(nameof(request.Id), "Asset not found!");

            // TODO: Validation for asset dependencies
            //       OR
            //       Send Notification to all users that have this asset (Asset {id} {name} is removed from our Company)

            await this.assetRepository.DeleteAsync(this.mapper.Map<DAL.Asset>(asset));

            return Unit.Value;
        }
    }
}
