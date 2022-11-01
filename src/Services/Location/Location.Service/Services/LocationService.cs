namespace Location.Service.Services
{
    using AutoMapper;
    using Location.Data.Repositories.Interfaces;
    using Location.Service.DTO;
    using Location.Service.Services.Interfaces;
    using Location.Service.UnitOfWork;
    using System.Threading.Tasks;
    using DTOs = Location.Data.DTO;

    public class LocationService : ILocationService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly ILocationRepository locationRepository;
        private readonly IMapper mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.locationRepository = locationRepository;
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<int> Create(CreateLocationRequest request)
        {
            using (IUnitOfWork unitOfWork = this.unitOfWorkFactory.Create())
            {
                var mappedRequest = this.mapper.Map<DTOs.CreateLocation>(request);
                // TODO: Set user id and organization Id;
                mappedRequest.UpdatedBy = 1;
                mappedRequest.OrganizationId = 1;

                var locationId = await this.locationRepository.CreateLocation(mappedRequest);

                unitOfWork.Commit();

                return locationId;
            }
        }

        public async Task CreateAssetLocation(Data.Entities.AssetLocation request)
        {
            using (IUnitOfWork unitOfWork = this.unitOfWorkFactory.Create())
            {
                await this.locationRepository.CreateAssetLocation(request);

                unitOfWork.Commit();
            }
        }

        public async Task Delete(int id)
        {
            // TODO: get all location  dependancies (Through GRPC to other services)
            // var locationToDelete = grpc.Request() ?? throw new NotFoundException("Location", id);

            //if (locationToDelete.HasDependencies)
            //    throw new BadRequestException(ErrorMessages.EntityWithDependenciesCannoteDeleted);

            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                await this.locationRepository.DeleteLocation(new DTOs.DeleteLocation(id));
                unitOfWork.Commit();
            }
        }

        public async Task<GetAllLocationsByOrganizationId> GetAllByOrganizationId(int organizationId)
        {
            var result = await this.locationRepository.GetAllByOrganizationId(organizationId);
            return this.mapper.Map<GetAllLocationsByOrganizationId>(result);
        }

        public async Task<Location> GetById(int id)
        {
            var result = await this.locationRepository.GetById(id);/*?? throw new NotFoundException("Location",id);*/
            return this.mapper.Map<Location>(result);
        }

        public async Task Update(UpdateLocationRequest request)
        {
            using (var unitOfWork = this.unitOfWorkFactory.Create())
            {
                var mappedRequest = this.mapper.Map<DTOs.UpdateLocation>(request);
                // TODO: Set user id and organization Id;
                mappedRequest.UpdatedBy = 1;
                mappedRequest.OrganizationId = 1;

                await this.locationRepository.UpdateLocation(mappedRequest);

                unitOfWork.Commit();
            }
        }
    }
}
