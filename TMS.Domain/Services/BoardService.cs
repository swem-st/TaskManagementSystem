
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TMS.Data;
using TMS.Data.Entities;
using TMS.Domain.ApiModels.RequestApiModels;
using TMS.Domain.ApiModels.ResponceApiModels;
using TMS.Domain.Errors;
using TMS.Domain.Interfaces;

namespace TMS.Domain.Services
{
    public class BoardService : IBoardService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public BoardService(DataContext context, IMapper mapper)
        {
            _dataContext = context;
            _mapper = mapper;
        }

        public async Task<BoardPostApiModel> CreateBoard(BoardPostApiModel board)
        {
            var Board = _mapper.Map<Board>(board);          
            _dataContext.Boards.Add(Board);
            await _dataContext.SaveChangesAsync();
            return _mapper.Map<BoardPostApiModel>(Board);      
        }

        public async Task<ResponseApiModel<HttpStatusCode>> DeleteBoard(Guid id)
        {
            var deletedDesk = await _dataContext.Boards.FindAsync(id);
            if (deletedDesk == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "You can't delete this board");
            }
            _dataContext.Boards.Remove(deletedDesk);
            await _dataContext.SaveChangesAsync();
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true , "Board was deleted");
        }

        public async Task<IEnumerable<BoardResponseApiModel>> GetAllBoards()
        {
            var boards = await _dataContext.Boards.ToListAsync();
            if (boards == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "You don't have the boards");
            }
            return _mapper.Map<IEnumerable<BoardResponseApiModel>>(boards);
        }

        public async Task<BoardResponseApiModel> GetBoardById(Guid id)
        {
            var board = await _dataContext.Boards.FindAsync(id);


            if (board == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Board not found");
            }

            return _mapper.Map<BoardResponseApiModel>(board);
        }

        public async Task<ResponseApiModel<HttpStatusCode>> UpdateBoard(BoardPutApiModel board)
        {
            var Board = _mapper.Map<Board>(board);
            if (Board != null)
            {
                _dataContext.Entry(Board).State = EntityState.Modified;
                await _dataContext.SaveChangesAsync();
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, "Board was updated");
            }
            else throw new RestException(HttpStatusCode.BadRequest, "You can't update the board");
        }
      
    }
}
