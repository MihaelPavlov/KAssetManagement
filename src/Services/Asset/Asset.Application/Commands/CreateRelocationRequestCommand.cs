namespace Asset.Application.Commands
{
    using AutoMapper;
    using Asset.Application.Persistence;
    using DAL = Asset.Domain.Entities;
    using System.ComponentModel.DataAnnotations;
    using MediatR;

    public class CreateRelocationRequestCommand : IRequest<int>
    {
        [Required]
        public int AssetId { get; set; }

        [Required]
        public int FromSiteId { get; set; }

        [Required]
        public int ToSiteId { get; set; }

        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public int FromLocationId { get; set; }
        public int ToLocationId { get; set; }
    }

    internal class CreateRelocationRequestCommandHandler : IRequestHandler<CreateRelocationRequestCommand, int>
    {
        private readonly IMapper mapper;
        private readonly IRelocationRepository relocationRepository;

        public CreateRelocationRequestCommandHandler(IRelocationRepository relocationRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.relocationRepository = relocationRepository;
        }

        public async Task<int> Handle(CreateRelocationRequestCommand request, CancellationToken cancellationToken)
        {
            var result = await this.relocationRepository.AddAsync(this.mapper.Map<DAL.RelocationRequest>(request));

            return result.Id;
        }
    }
}
