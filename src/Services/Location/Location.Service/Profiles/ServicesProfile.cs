namespace Location.Service.Profiles
{
    using Asset.EventBus.Messages.Events;
    using AutoMapper;
    using DAL = Location.Data.Entities;
    using Location.Service.DTO;
    using LDD = Location.Data.DTO;

    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<LDD.GetLocationById, Location>();
            CreateMap<LDD.UpdateLocation, UpdateLocationRequest>();
            CreateMap<LDD.GetAllLocationsByOrganizationId, GetAllLocationsByOrganizationId>();
            CreateMap<LDD.LocationResultDTO, Location>();
            CreateMap<CreateLocationRequest, LDD.CreateLocation>();
            CreateMap<UpdateLocationRequest, LDD.UpdateLocation>();
            CreateMap<DAL.AssetLocation, AssetCreateLocationEvent>().ReverseMap();
        }
    }
}
