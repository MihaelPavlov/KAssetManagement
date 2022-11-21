namespace Asset.Application.Commands.Relocation
{
    using Asset.Application.Persistence;
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateRelocationRequestStatusCommand : IRequest
    {
        public int Id { get; set; }
        public int RequestStatus { get; set; }

        public UpdateRelocationRequestStatusCommand(int id, int requestStatus)
        {
            this.Id = id;
            this.RequestStatus = requestStatus;
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
            var relocationRequest= await this.relocationRepository.GetByIdAsync(request.Id);

            relocationRequest.Status = request.RequestStatus;
           
            await this.relocationRepository.UpdateAsync(relocationRequest);

            return Unit.Value;
        }
    }
}
