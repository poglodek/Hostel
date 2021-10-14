using System;
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
            }).CreateMapper();
        }

        public RegisterUserModel MapToModel(RegisterUserDto registerUser) =>
            _mapper.Map<RegisterUserModel>(registerUser);
        public RegisterUserDto MapToDto(RegisterUserModel registerUser) =>
            _mapper.Map<RegisterUserDto>(registerUser);

    }
}
