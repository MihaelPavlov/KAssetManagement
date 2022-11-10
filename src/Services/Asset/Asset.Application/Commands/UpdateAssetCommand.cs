namespace Asset.Application.Commands
{
    using Asset.Application.Persistence;
    using AutoMapper;
    using MediatR;
    using System.ComponentModel.DataAnnotations;
    using DAL = Asset.Domain.Entities;

    public class UpdateAssetCommand : IRequest
    {
        [Required]
        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        public int GuarantyMounts { get; set; }
        public string? Producer { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public decimal? Price { get; set; }
        public int Type { get; set; }
        public int PeriodType { get; set; }
        public int Status { get; set; }
    }

    internal class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand>
    {
        private readonly IMapper mapper;
        private readonly IAssetRepository assetRepository;

        public UpdateAssetCommandHandler(IAssetRepository assetRepository, IMapper mapper)
        {
            this.assetRepository = assetRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            await this.assetRepository.UpdateAsync(this.mapper.Map<DAL.Asset>(request));

            return Unit.Value;
        }
    }
}
