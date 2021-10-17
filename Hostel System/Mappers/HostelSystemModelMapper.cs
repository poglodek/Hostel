﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            }).CreateMapper();
        }

        public RegisterUserModel MapToModel(RegisterUserDto registerUser) =>
            _mapper.Map<RegisterUserModel>(registerUser);
        public RegisterUserDto MapToDto(RegisterUserModel registerUser) =>
            _mapper.Map<RegisterUserDto>(registerUser);
        public LoginModel MapToModel(LoginDto login) =>
            _mapper.Map<LoginModel>(login);
        public LoginDto MapToDto(LoginModel login) =>
            _mapper.Map<LoginDto>(login); 
        public RoomModel MapToModel(RoomDto login) =>
            _mapper.Map<RoomModel>(login);
        public RoomDto MapToDto(RoomModel login) =>
            _mapper.Map<RoomDto>(login);
        public RoomReservationModel MapToModel(RoomModel login) =>
            _mapper.Map<RoomReservationModel>(login);
        public RoomDto MapToModel(RoomReservationModel login) =>
            _mapper.Map<RoomDto>(login);
        public IEnumerable<RoomModel> MapToModel(IEnumerable<RoomDto> login) =>
            _mapper.Map<IEnumerable<RoomModel>>(login);
        public IEnumerable<RoomDto> MapToDto(IEnumerable<RoomModel> login) =>
            _mapper.Map<IEnumerable<RoomDto>>(login);

    }
}
