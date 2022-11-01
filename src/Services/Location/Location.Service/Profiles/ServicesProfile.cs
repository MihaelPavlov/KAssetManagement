namespace Location.Service.Profiles
{
    using Asset.EventBus.Messages.Events;
    using AutoMapper;
    using DAL = Location.Data.Entities;
    using Location.Service.DTO;
    using DTOs = Location.Data.DTO;

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
            CreateMap<DAL.AssetLocation, AssetCreateLocationEvent>().ReverseMap();
        }
    }
}
