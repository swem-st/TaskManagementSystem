using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TMS.Domain.ApiModels.RequestApiModels;
using TMS.Domain.ApiModels.ResponceApiModels;
using TMS.Domain.Errors;
using TMS.Domain.Interfaces;
using TMS.WebApi.Controllers;
using Xunit;


      
namespace Tms.UnitTests.WebApi.Controllers
{
    public class BoardControllerUnitTest
    {
        private readonly Mock<IBoardService> _boardServices = new Mock<IBoardService>();
        private readonly BoardController boardController;
            
       public BoardControllerUnitTest()
        {
            boardController = new BoardController(_boardServices.Object);
        }

        [Fact]
        public async Task GetAllBoards_ResultSuccess() 
        {
            //Arrange
            _boardServices.Setup(repo => repo.GetAllBoards()).ReturnsAsync(new List<BoardResponseApiModel>() { });

            //Act
            var result = await boardController.GetAllBoards() as OkObjectResult;
            var resultValue = result.Value as List<BoardResponseApiModel>;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<BoardResponseApiModel>>(resultValue);
        }

        [Fact]
        public async Task GetAllBoards_ResultFailed()
        {
            //Arrange
            _boardServices.Setup(repo => repo.GetAllBoards()).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => boardController.GetAllBoards();

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);

        }
        [Fact]
        public async Task GetBoardById_ResultSuccess()
        {
            //Arrange
            _boardServices.Setup(repo => repo.GetBoardById(It.IsAny<Guid>())).ReturnsAsync(new BoardResponseApiModel() { });

            //Act
            var result = await boardController.GetBoardById(It.IsAny<Guid>()) as OkObjectResult;
            var resultValue = result.Value as BoardResponseApiModel;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<BoardResponseApiModel>(resultValue);
        }

        [Fact]
        public async Task GetBoardById_ResultFailed()
        {
            //Arrange
            _boardServices.Setup(repo => repo.GetBoardById(It.IsAny<Guid>())).ThrowsAsync(new RestException(HttpStatusCode.NotFound, "Failed"));

            //Act
            Func<Task> act = () => boardController.GetBoardById(It.IsAny<Guid>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task CreateBoard_ResultSuccess()
        {
            //Arrange
            _boardServices.Setup(repo => repo.CreateBoard(It.IsAny<BoardPostApiModel>())).ReturnsAsync(new BoardPostApiModel() { });

            //Act
            var result = await boardController.CreateBoard(It.IsAny<BoardPostApiModel>()) as OkObjectResult;
            var resultValue = result.Value as BoardPostApiModel;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<BoardPostApiModel>(resultValue);
        }

        [Fact]
        public async Task CreateBoard_ResultFailed()
        {
            //Arrange
            _boardServices.Setup(repo => repo.CreateBoard(It.IsAny<BoardPostApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => boardController.CreateBoard(It.IsAny<BoardPostApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task UpdateBoard_ResultSuccess()
        {
            //Arrange
            _boardServices.Setup(repo => repo.UpdateBoard(It.IsAny<BoardPutApiModel>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await boardController.UpdateBoard(It.IsAny<BoardPutApiModel>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task UpdateBoard_ResultFailed()
        {
            //Arrange
            _boardServices.Setup(repo => repo.UpdateBoard(It.IsAny<BoardPutApiModel>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => boardController.UpdateBoard(It.IsAny<BoardPutApiModel>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }

        [Fact]
        public async Task DeleteBoard_ResultSuccess()
        {
            //Arrange
            _boardServices.Setup(repo => repo.DeleteBoard(It.IsAny<Guid>())).ReturnsAsync(new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Success"));

            //Act
            var result = await boardController.DeleteBoard(It.IsAny<Guid>()) as OkObjectResult;
            var resultValue = result.Value as ResponseApiModel<HttpStatusCode>;

            //Assert
            Assert.IsType<ResponseApiModel<HttpStatusCode>>(resultValue);
            Assert.Equal("Success", resultValue.Message);
            Assert.True(resultValue.Success);
            Assert.Equal(HttpStatusCode.OK, resultValue.Data);
        }

        [Fact]
        public async Task DeleteBoard_ResultFailed()
        {
            //Arrange
            _boardServices.Setup(repo => repo.DeleteBoard(It.IsAny<Guid>())).ThrowsAsync(new RestException(HttpStatusCode.BadRequest, "Failed"));

            //Act
            Func<Task> act = () => boardController.DeleteBoard(It.IsAny<Guid>());

            //Assert
            var result = await Assert.ThrowsAsync<RestException>(act);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("Failed", result.Message);
        }
    }
}
