namespace Location.Service.Profiles
{
    using AutoMapper;
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
        }
    }
}
