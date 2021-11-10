using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using TMS.Domain.Interfaces;
using TMS.Domain.ApiModels.RequestApiModels;

namespace TMS.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskModelService _taskService;
        public TaskController(ITaskModelService taskService)
        {
            _taskService = taskService;
        }   

        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById(Guid taskId)
        {
            var response = await _taskService.GetTaskById(taskId);           
            return Ok(response);
        }

        [HttpGet("GetAllTasksOfBoard")]
        public async Task<IActionResult> GetAllTasksOfBoard(Guid boardId)
        {
            var response = await _taskService.GetAllTasksOfBoard(boardId);
            return Ok(response);
        }

        [HttpGet("GetAllTasksByProgress")]
        public async Task<IActionResult> GetAllTasksByProgress(Guid boardId, string progressName)
        {
            var response = await _taskService.GetAllTasksByProgress(boardId, progressName);
            return Ok(response);
        }

        [HttpPost("CreateTask")]
        public async Task<ActionResult> CreateTask(TaskPostRequestApiModel taskModel, Guid boardId)
        {
            var response = await _taskService.CreateTask(taskModel,boardId);
            return Ok(response);
        }

        [HttpPost("CreateSubTask")]
        public async Task<ActionResult> CreateSubTask(SubTaskCutRequestApiModel subTaskModel, Guid taskId)
        {
            var response = await _taskService.CreateSubTask(subTaskModel, taskId);
            return Ok(response);
        }

        [HttpPut("ChangeTask")]
        public async Task<ActionResult> ChangeTask(TaskPutRequestApiModel taskModel)
        {
            var response = await _taskService.ChangeTask(taskModel);
            return Ok(response);
        }

        [HttpPut("ChangeSubTask")]
        public async Task<ActionResult> ChangeSubTask(SubTaskPutRequestApiModel subTaskModel)
        {
            var response = await _taskService.ChangeSubTask(subTaskModel);
            return Ok(response);
        }      

        [HttpDelete("DeleteTask")]
        public async Task<ActionResult> DeleteTask(Guid taskId)
        {
            var response=  await _taskService.DeleteTask(taskId);
            return Ok(response);
        }

        [HttpDelete("DeleteSubTask")]
        public async Task<ActionResult> DeleteSubTask(Guid subTaskId)
        {
            var response = await _taskService.DeleteSubTask(subTaskId);
            return Ok(response);
        }


    }
}