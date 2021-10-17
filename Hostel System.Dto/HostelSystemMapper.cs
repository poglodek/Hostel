using AutoMapper;
using Hostel_System.Database.Entity;
using Hostel_System.Dto.Dto;


namespace Hostel_System.Dto
{
    public class HostelSystemMapper : Profile
    {
        public HostelSystemMapper()
        {
            CreateMap<RegisterUserDto, User>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
        }
    }
}
