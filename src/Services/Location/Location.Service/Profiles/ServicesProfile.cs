namespace Location.Service.Profiles
{
    using AutoMapper;
    using DAL = Location.Data.Entities;
    using Location.Service.DTO;
    using DTOs = Location.Data.DTO;
    using EventBus.Messages.AssetEvents;

    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<DTOs.GetLocationById, Location>();
            CreateMap<DTOs.UpdateLocation, UpdateLocationRequest>();
            CreateMap<DTOs.GetAllLocationsByOrganizationId, GetAllLocationsByOrganizationId>();
            CreateMap<DTOs.LocationResultDTO, Location>();
            CreateMap<CreateLocationRequest, DTOs.CreateLocation>();
            CreateMap<UpdateLocationRequest, DTOs.UpdateLocation>();

            // Events
            CreateMap<DAL.AssetLocation, CreateAssetLocationEvent>().ReverseMap();
        }
    }
}
