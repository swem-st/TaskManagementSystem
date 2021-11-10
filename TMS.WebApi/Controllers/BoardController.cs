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
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardController (IBoardService boardService)
        {
            _boardService = boardService;
        }
        

        [HttpGet("GetAllBoards")]
        public async Task<IActionResult> GetAllBoards()
        {
            var response = await _boardService.GetAllBoards();           
            return Ok(response);
        }

        [HttpGet("GetBoardById")]
        public async Task<ActionResult> GetBoardById(Guid id)
        {
            var response = await _boardService.GetBoardById(id);
            return Ok(response);
        }

        [HttpPost("CreateBoard")]
        public async Task<ActionResult> CreateBoard(BoardPostApiModel board)
        {
            var response = await _boardService.CreateBoard(board);
            return Ok(response);
        }
        
        [HttpPut("EditBoard")]
        public async Task<ActionResult> UpdateBoard(BoardPutApiModel board)
        {
            var response = await _boardService.UpdateBoard(board);
            return Ok(response);
        }

        [HttpDelete("DeleteBoard")]
        public async Task<ActionResult> DeleteBoard(Guid id)
        {
            var response=  await _boardService.DeleteBoard(id);
            return Ok(response);
        }


    }
}