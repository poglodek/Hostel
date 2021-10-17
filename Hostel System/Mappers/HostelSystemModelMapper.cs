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
                config.CreateMap<RoomDto, RoomReservationModel>().ReverseMap();
                config.CreateMap<RoomReservationDto, RoomReservationModel>().ReverseMap();
            }).CreateMapper();
        }

        public T Map<T>(object obj) => _mapper.Map<T>(obj);

    }
}
