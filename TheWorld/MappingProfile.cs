using AutoMapper;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<TripViewModel, Trip>();
            CreateMap<Trip, TripViewModel>();
            CreateMap<StopViewModel, Stop>();
            CreateMap<Stop, StopViewModel>();
        }
    }
}