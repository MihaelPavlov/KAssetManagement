namespace Asset.Application.Commands.Relocation
{
    using Asset.Application.Persistence;
    using Asset.Domain.Entities;
    using Asset.Domain.Enums;
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateRelocationRequestStatusCommand : IRequest
    {
        public int Id { get; set; }

        public UpdateRelocationRequestStatusCommand(int id)
        {
            this.Id = id;
        }
    }

    internal class UpdateRelocationRequestStatusCommandHandler : IRequestHandler<UpdateRelocationRequestStatusCommand>
    {
        private readonly IMapper mapper;
        private readonly IRelocationRepository relocationRepository;

        public UpdateRelocationRequestStatusCommandHandler(IRelocationRepository relocationRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.relocationRepository = relocationRepository;
        }

        public async Task<Unit> Handle(UpdateRelocationRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var mappedRequest = this.mapper.Map<RelocationRequest>(request);
            mappedRequest.Status = (int)RequestStatus.Approved;

            await this.relocationRepository.UpdateAsync(this.mapper.Map<RelocationRequest>(request));

            return Unit.Value;
        }
    }
}
