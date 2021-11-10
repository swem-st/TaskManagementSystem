using AutoMapper;
using TMS.Data.Entities;
using TMS.Domain.ApiModels.RequestApiModels;
using TMS.Domain.ApiModels.ResponceApiModels;

namespace TMS.Domain.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Board, BoardPostApiModel>().ReverseMap();
            CreateMap<Board, BoardPutApiModel>().ReverseMap();
            CreateMap<Board, BoardResponseApiModel>().ReverseMap();
            CreateMap<TaskPostRequestApiModel, TaskModel>()
                .ForMember(x => x.Progress, org => org.Ignore())
                .ForMember(x => x.BoardId, org => org.Ignore())
                .ForMember(x => x.Board, org => org.Ignore())
                .ForMember(s => s.SubTaskModels, org => org.MapFrom(src => src.SubTaskModel))
                .ReverseMap();
            CreateMap<TaskPutRequestApiModel, TaskModel>()
               .ForMember(x => x.Progress, org => org.Ignore())
               .ReverseMap();
            CreateMap<TaskModel, TaskResponseApiModel>().ReverseMap();
            CreateMap<SubTaskPutRequestApiModel, SubTaskModel>().ReverseMap();
            CreateMap<SubTaskModel, SubTaskCutRequestApiModel>().ReverseMap();
            CreateMap<SubTaskModel, SubTaskResponseApiModel>().ReverseMap();
            CreateMap<Progress, ProgressResponseApiModel>();
        }
    }

}

