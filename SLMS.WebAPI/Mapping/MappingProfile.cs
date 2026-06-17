using AutoMapper;
using SLMS.DOL.Entities;
using SLMS.Shared.DTOs;

namespace SLMS.WebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Request
            CreateMap<Request, RequestResponseDTO>();
            CreateMap<RequestCreateDTO, Request>();

            // Book Issue
            CreateMap<BookIssue, BookIssueResponseDTO>();
        }
    }
}