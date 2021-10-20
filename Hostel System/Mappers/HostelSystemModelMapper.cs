using AutoMapper;
using Hostel_System.Dto.Dto;
using Hostel_System.Model;

namespace Hostel_System.Mappers
{
    public class HostelSystemModelMapper
    {
        IMapper _mapper;
        public HostelSystemModelMapper()
        {
            _mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<RegisterUserModel, RegisterUserDto>().ReverseMap();
                config.CreateMap<LoginModel, LoginDto>().ReverseMap();
                config.CreateMap<RoomModel, RoomDto>().ReverseMap();
                config.CreateMap<RoomModel, RoomReservationModel>().ReverseMap();
                config.CreateMap<RoomDto, RoomReservationModel>().ReverseMap();
                config.CreateMap<UserDto, UserModel>().ReverseMap();
                config.CreateMap<RoomReservationDto, RoomReservationModel>().ReverseMap();
                config.CreateMap<ChangePasswordDto, ChangePasswordModel>().ReverseMap();
                config.CreateMap<RoomReservedDto, RoomReservedModel>()
                    .ForMember(x=> x.BookingUser,z=>z.MapFrom(c=>c.BookingUser))
                    .ReverseMap();
                config.CreateMap<ReservedInfoModel, ReservedInfoDto>()
                    .ForMember(x=> x.BookingUser,z=>z.MapFrom(c=>c.BookingUser))
                    .ReverseMap();
            }).CreateMapper();
        }

        public T Map<T>(object obj) => _mapper.Map<T>(obj);

    }
}
