using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TMS.Domain.ApiModels.RequestApiModels;
using TMS.Domain.ApiModels.ResponceApiModels;

namespace TMS.Domain.Interfaces
{
    public interface ITaskModelService
    {
        Task<TaskResponseApiModel> GetTaskById(Guid taskId);
        Task<IEnumerable<TaskResponseApiModel>> GetAllTasksOfBoard(Guid boardId);
        Task<IEnumerable<TaskResponseApiModel>> GetAllTasksByProgress(Guid boardId, string progressName);
        Task<ResponseApiModel<HttpStatusCode>> CreateTask(TaskPostRequestApiModel taskModel, Guid boardId);
        Task<ResponseApiModel<HttpStatusCode>> CreateSubTask(SubTaskCutRequestApiModel subTaskModel, Guid taskId);
        Task<ResponseApiModel<HttpStatusCode>> ChangeTask(TaskPutRequestApiModel taskModel) ;
        Task<ResponseApiModel<HttpStatusCode>> ChangeSubTask(SubTaskPutRequestApiModel subTaskModel);
        Task<ResponseApiModel<HttpStatusCode>> DeleteTask(Guid taskId);
        Task<ResponseApiModel<HttpStatusCode>> DeleteSubTask(Guid subTaskId);
    }
}
