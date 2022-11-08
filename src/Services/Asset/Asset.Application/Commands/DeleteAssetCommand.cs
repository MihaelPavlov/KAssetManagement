namespace Asset.Application.Commands
{
    using Asset.Application.Exceptions;
    using Asset.Application.Persistence;
    using Asset.Application.Queries;
    using AutoMapper;
    using MediatR;
    using System.Diagnostics.CodeAnalysis;
    using DAL = Asset.Domain.Entities;

    public class DeleteAssetCommand : IRequest
    {
        public int AssetId { get; set; }

        public DeleteAssetCommand(int assetId)
        {
            AssetId = assetId;
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
            var asset = await this.mediator.Send(new GetAssetByIdQuery(request.AssetId));

            if (asset == null)
                throw new NotFoundException(nameof(request.AssetId), "Asset not found!");

            // TODO: Validation for asset dependencies

            await this.assetRepository.DeleteAsync(this.mapper.Map<DAL.Asset>(asset));

            return Unit.Value;
        }
    }
}
