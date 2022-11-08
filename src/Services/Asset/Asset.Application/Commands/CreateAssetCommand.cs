namespace Asset.Application.Commands
{
    using MediatR;
    using AutoMapper;
    using Asset.Application.Persistence;
    using DAL = Asset.Domain.Entities;
    using System.ComponentModel.DataAnnotations;

    public class CreateAssetCommand : IRequest<int>
    {
        [Required]
        public int InventoryNumber { get; set; }

        [Required]
        public int GuarantyMounts { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public string? Producer { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Model { get; set; }

        [Required]
        public string? Price { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int PeriodType { get; set; }

        [Required]
        public int Status { get; set; }
    }

    internal class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, int>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IAssetRepository assetRepository;

        public CreateAssetCommandHandler(IAssetRepository assetRepository, IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.assetRepository = assetRepository;
        }

        public async Task<int> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var result = await assetRepository.AddAsync(mapper.Map<DAL.Asset>(request));

            // publish inside location service
            await this.mediator.Send(new CreateAssetLocationCommand() { AssetId = result.Id, LocationId = result.LocationId, CreationDate = DateTime.UtcNow });

            return result.Id;
        }
    }
}
