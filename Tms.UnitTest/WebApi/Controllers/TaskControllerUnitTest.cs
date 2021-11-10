using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TMS.Domain.ApiModels.RequestApiModels;
using TMS.Domain.ApiModels.ResponceApiModels;
using TMS.Domain.Errors;
using TMS.Domain.Interfaces;
using TMS.WebApi.Controllers;
using Xunit;

namespace Tms.UnitTests.WebApi.Controllers
{
    public class TaskControllerUnitTest
    {
        private readonly Mock<ITaskModelService> _taskServices = new Mock<ITaskModelService>();
        private readonly TaskController taskController;
        public TaskControllerUnitTest()
        {
            taskController = new TaskController(_taskServices.Object);
        }
        
        [Fact]
        public async Task GetTaskById_ResultSuccess()
        {
            //Arrange
            _taskServices.Setup(repo => repo.GetTaskById(It.IsAny<Guid>())).ReturnsAsync(new TaskResponseApiModel() { });

            //Act
            var result = await taskController.GetTaskById(It.IsAny<Guid>()) as OkObjectResult;
            var resultValue = result.Value as TaskResponseApiModel;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<TaskResponseApiModel>(resultValue);
        }

        [Fact]
        public async Task GetTaskById_ResultFailed()
        {
            //Arrange
            _taskServices.Setup(repo => repo.GetTaskById(It.IsAny<Guid>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => taskController.GetTaskById(It.IsAny<Guid>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetAllTasksOfBoard_ResultSuccess()
        {
            //Arrange
            _taskServices.Setup(repo => repo.GetAllTasksOfBoard(It.IsAny<Guid>())).ReturnsAsync(new List<TaskResponseApiModel>() { });

            //Act
            var result = await taskController.GetAllTasksOfBoard(It.IsAny<Guid>()) as OkObjectResult;
            var resultValue = result.Value as List<TaskResponseApiModel>;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<TaskResponseApiModel>>(resultValue);
        }

        [Fact]
        public async Task GetAllTasksOfBoard_ResultFailed()
        {
            //Arrange
            _taskServices.Setup(repo => repo.GetAllTasksOfBoard(It.IsAny<Guid>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => taskController.GetAllTasksOfBoard(It.IsAny<Guid>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task GetAllTasksByProgress_ResultSuccess()
        {
            //Arrange
            _taskServices.Setup(repo => repo.GetAllTasksByProgress(It.IsAny<Guid>() , It.IsAny<string>())).ReturnsAsync(new List<TaskResponseApiModel>() { });

            //Act
            var result = await taskController.GetAllTasksByProgress(It.IsAny<Guid>(), It.IsAny<string>()) as OkObjectResult;
            var resultValue = result.Value as List<TaskResponseApiModel>;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<TaskResponseApiModel>>(resultValue);
        }

        [Fact]
        public async Task GetAllTasksByProgress_ResultFailed()
        {
            //Arrange
            _taskServices.Setup(repo => repo.GetAllTasksByProgress(It.IsAny<Guid>(), It.IsAny<string>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => taskController.GetAllTasksByProgress(It.IsAny<Guid>(), It.IsAny<string>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task CreateTask_ResultSuccess()
        {
            //Arrange
            _taskServices.Setup(repo => repo.CreateTask(It.IsAny<TaskPostRequestApiModel>(), It.IsAny<Guid>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await taskController.CreateTask(It.IsAny<TaskPostRequestApiModel>(), It.IsAny<Guid>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task CreateTask_ResultFailed()
        {
            //Arrange
            _taskServices.Setup(repo => repo.CreateTask(It.IsAny<TaskPostRequestApiModel>() , It.IsAny<Guid>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => taskController.CreateTask(It.IsAny<TaskPostRequestApiModel>(), It.IsAny<Guid>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task CreateSubTask_ResultSuccess()
        {
            //Arrange
            _taskServices.Setup(repo => repo.CreateSubTask(It.IsAny<SubTaskCutRequestApiModel>(), It.IsAny<Guid>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await taskController.CreateSubTask(It.IsAny<SubTaskCutRequestApiModel>(), It.IsAny<Guid>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task CreateSubTask_ResultFailed()
        {
            //Arrange
            _taskServices.Setup(repo => repo.CreateSubTask(It.IsAny<SubTaskCutRequestApiModel>(), It.IsAny<Guid>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => taskController.CreateSubTask(It.IsAny<SubTaskCutRequestApiModel>(), It.IsAny<Guid>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task ChangeTask_ResultSuccess()
        {
            //Arrange
            _taskServices.Setup(repo => repo.ChangeTask(It.IsAny<TaskPutRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await taskController.ChangeTask(It.IsAny<TaskPutRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task ChangeTask_ResultFailed()
        {
            //Arrange
            _taskServices.Setup(repo => repo.ChangeTask(It.IsAny<TaskPutRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => taskController.ChangeTask(It.IsAny<TaskPutRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task ChangeSubTask_ResultSuccess()
        {
            //Arrange
            _taskServices.Setup(repo => repo.ChangeSubTask(It.IsAny<SubTaskPutRequestApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await taskController.ChangeSubTask(It.IsAny<SubTaskPutRequestApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task ChangeSubTask_ResultFailed()
        {
            //Arrange
            _taskServices.Setup(repo => repo.ChangeSubTask(It.IsAny<SubTaskPutRequestApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => taskController.ChangeSubTask(It.IsAny<SubTaskPutRequestApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task DeleteTask_ResultSuccess()
        {
            //Arrange
            _taskServices.Setup(repo => repo.DeleteTask(It.IsAny<Guid>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await taskController.DeleteTask(It.IsAny<Guid>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task DeleteTask_ResultFailed()
        {
            //Arrange
            _taskServices.Setup(repo => repo.DeleteTask(It.IsAny<Guid>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => taskController.DeleteTask(It.IsAny<Guid>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }
    }
}
