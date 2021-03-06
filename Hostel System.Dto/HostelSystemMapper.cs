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
            CreateMap<User, UserDto>()
                .ForMember(x => x.RoleName, z => z.MapFrom(c => c.RoleName.RoleName))
                .ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<BookToGuestDto, Reservation>().ReverseMap();
            CreateMap<RoomReservationDto, Reservation>().ReverseMap();
            CreateMap<Reservation, RoomReservedDto>().ReverseMap();
            CreateMap<Reservation, ReservedInfoDto>()
                .ForMember(x => x.RoomName, z => z.MapFrom(c => c.BookingRoom.RoomName))
                .ReverseMap();
        }
    }
}
