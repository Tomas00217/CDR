using AutoMapper;
using CDR.Models;
using CDR.Models.Dtos;

namespace CDR.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CallDetailRecordModel, CallDetailRecordDto>();
    }
}
