using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Net;
using TMS.Domain.Interfaces;
using TMS.Data.Entities;
using TMS.Domain.ApiModels.ResponceApiModels;
using TMS.Domain.Errors;
using TMS.Domain.ApiModels.RequestApiModels;
using TMS.Data;

namespace TMS.Domain.Services
{
    public class TaskModelService : ITaskModelService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public TaskModelService(DataContext context, IMapper mapper)
        {
            _dataContext = context;
            _mapper = mapper;
        }
        public async Task<TaskResponseApiModel> GetTaskById(Guid taskId)
        {

            var Task = await _dataContext.TaskModels.Include(s => s.Progress).Include(s => s.Board).Include(a => a.SubTaskModels).FirstOrDefaultAsync(x => x.Id == taskId);

            if (Task == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Task not found");
            }
            return _mapper.Map<TaskResponseApiModel>(Task);

        }

        public async Task<IEnumerable<TaskResponseApiModel>> GetAllTasksByProgress(Guid boardId, string progressName)
        {
            var board = await _dataContext.Boards.FindAsync(boardId);
            if (board == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Board not found");
            }

            var tasks = await _dataContext.TaskModels.Include(s => s.Progress).Include(a => a.SubTaskModels)
                 .Where(x => x.Board == board && x.Progress.ProgressName == progressName)
                .ToListAsync();


            if (tasks == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "You don't have task");
            }

            return _mapper.Map<IEnumerable<TaskResponseApiModel>>(tasks);
        }

        public async Task<IEnumerable<TaskResponseApiModel>> GetAllTasksOfBoard(Guid boarId)
        {
            var board = await _dataContext.Boards.FindAsync(boarId);

            var tasks = await _dataContext.TaskModels.Include(s => s.Progress).Include(a => a.SubTaskModels).Where(x => x.Board == board).ToListAsync();

            if (tasks == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "You don't have the boards");
            }

            return _mapper.Map<IEnumerable<TaskResponseApiModel>>(tasks);
        }

        public async Task<ResponseApiModel<HttpStatusCode>> ChangeTask(TaskPutRequestApiModel taskModel)
        {           
            var progress = await _dataContext.Progresses.ToListAsync();
            var Task = _mapper.Map<TaskModel>(taskModel);
            Task.Progress = progress.FirstOrDefault(x => x.ProgressName == taskModel.Progress);
            if (Task != null)
            {
                _dataContext.Attach(Task);
                _dataContext.Entry(Task).Property(p => p.Name).IsModified = true;
                _dataContext.Entry(Task).Property(p => p.Description).IsModified = true;
                _dataContext.Entry(Task).Property(p => p.StartDate).IsModified = true;
                _dataContext.Entry(Task).Property(p => p.FinishDate).IsModified = true;
                _dataContext.Entry(Task).Property(p => p.ProgressId).IsModified = true;
                await _dataContext.SaveChangesAsync();
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Task was updated");
            }
            else throw new RestException(HttpStatusCode.BadRequest, "You can't update the Task");
        }

        public async Task<ResponseApiModel<HttpStatusCode>> ChangeSubTask(SubTaskPutRequestApiModel subTaskModel)
        {
            var SubTaskUpdate = _mapper.Map<SubTaskModel>(subTaskModel);
            if (SubTaskUpdate != null)
            {
                _dataContext.Attach(SubTaskUpdate);
                _dataContext.Entry(SubTaskUpdate).Property(p => p.Name).IsModified = true;
                _dataContext.Entry(SubTaskUpdate).Property(p => p.Description).IsModified = true;
                _dataContext.Entry(SubTaskUpdate).Property(p => p.Done).IsModified = true;
                await _dataContext.SaveChangesAsync();
            }

            var ProgressCompleted = await _dataContext.Progresses.FirstOrDefaultAsync(x => x.ProgressName == "Completed");
            var ProgressPlanned = await _dataContext.Progresses.FirstOrDefaultAsync(x => x.ProgressName == "Planned");
            var ProgressInProgress = await _dataContext.Progresses.FirstOrDefaultAsync(x => x.ProgressName == "InProgress");

            if (SubTaskUpdate != null) 
            {
                var SubTask = await _dataContext.SubTaskModels.AsNoTracking().FirstOrDefaultAsync(x => x.Id == subTaskModel.Id);
                var task = await _dataContext.TaskModels.Include(a => a.SubTaskModels).FirstOrDefaultAsync(s => s.Id == SubTask.TaskModelId);
                
                int completed = 0;
                int planned = 0;
                if (subTaskModel.Done == true) { completed++; } 
                else { planned++; }
                foreach (var subTask in task.SubTaskModels.ToList())
                {              
                    if (subTask.Done == true) { completed++; }
                    else { planned++; }
                }
                if (completed == 0) { task.ProgressId = ProgressPlanned.Id; }
                else if (planned == 0) { task.ProgressId = ProgressCompleted.Id; }
                else { task.ProgressId = ProgressInProgress.Id; }

                _dataContext.Attach(task);
                _dataContext.Entry(task).Property(p => p.ProgressId).IsModified = true;
                await _dataContext.SaveChangesAsync();
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "SubTask was updated");
            }
            else throw new RestException(HttpStatusCode.BadRequest, "You can't update the SubTask");
        }      

        public async Task<ResponseApiModel<HttpStatusCode>> CreateSubTask(SubTaskCutRequestApiModel subTaskModel, Guid taskId)
        {
            var task = await _dataContext.TaskModels.FindAsync(taskId);

            if (task == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Task not found");
            }
            var SubTask = _mapper.Map<SubTaskModel>(subTaskModel);
            SubTask.TaskModel = task;
            if (SubTask != null)
            {
                _dataContext.Add(SubTask);
                await _dataContext.SaveChangesAsync();
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Task was created");
            }
            else throw new RestException(HttpStatusCode.BadRequest, "You can't create;' the Task");
        }

        public async Task<ResponseApiModel<HttpStatusCode>> CreateTask(TaskPostRequestApiModel taskModel, Guid boardId)
        {
            var board = await _dataContext.Boards.FindAsync(boardId);
            if (board == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Board not found");
            }
            var progress = await _dataContext.Progresses.ToListAsync();
            var Task = _mapper.Map<TaskModel>(taskModel);
            Task.Board = board;
            Task.Progress = progress.FirstOrDefault(x => x.ProgressName == taskModel.Progress);
            if (Task != null)
            {
                _dataContext.Add(Task);
                await _dataContext.SaveChangesAsync();
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Task was created");
            }
            else throw new RestException(HttpStatusCode.BadRequest, "You can't create;' the Task");
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteSubTask(Guid subTaskId)
        {
            var deletedSubTask = await _dataContext.SubTaskModels.FindAsync(subTaskId);

            if (deletedSubTask == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "SubTask not found");
            }

            _dataContext.SubTaskModels.Remove(deletedSubTask);
            await _dataContext.SaveChangesAsync();
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Task was deleted");
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteTask(Guid taskId)
        {
            var deletedTask = await _dataContext.TaskModels.Include(a => a.SubTaskModels).FirstOrDefaultAsync(s=>s.Id==taskId);

            if (deletedTask == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Task not found");
            }

            _dataContext.TaskModels.Remove(deletedTask);
            await _dataContext.SaveChangesAsync();
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Task was deleted");
        }


    }
}
