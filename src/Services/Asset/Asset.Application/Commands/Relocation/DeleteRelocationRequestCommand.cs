namespace Asset.Application.Commands.Relocation
{
    using Asset.Application.Exceptions;
    using Asset.Application.Persistence;
    using Asset.Domain.Enums;
    using MediatR;

    public class DeleteRelocationRequestCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteRelocationRequestCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteRelocationRequestCommandHandler : IRequestHandler<DeleteRelocationRequestCommand>
    {
        private readonly IRelocationRepository relocationRepository;

        public DeleteRelocationRequestCommandHandler(IRelocationRepository relocationRepository)
        {
            this.relocationRepository = relocationRepository;
        }

        public async Task<Unit> Handle(DeleteRelocationRequestCommand request, CancellationToken cancellationToken)
        {
            var relocationRequest = await this.relocationRepository.GetByIdAsync(request.Id);

            // Status 
            if (relocationRequest.Status != (int)RequestStatus.Pending && relocationRequest.GetRequest != null && relocationRequest.Received != null)
                throw new BadRequestException("Entity With Dependencies Cannot BeDeleted !");

            await this.relocationRepository.DeleteAsync(relocationRequest);

            return Unit.Value;
        }
    }
}
