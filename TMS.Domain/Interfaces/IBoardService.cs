using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TMS.Domain.ApiModels.RequestApiModels;
using TMS.Domain.ApiModels.ResponceApiModels;

namespace TMS.Domain.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardResponseApiModel>> GetAllBoards();
        Task<BoardResponseApiModel> GetBoardById(Guid id);
        Task<BoardPostApiModel> CreateBoard(BoardPostApiModel board);
        Task<ResponseApiModel<HttpStatusCode>> UpdateBoard(BoardPutApiModel board);
        Task<ResponseApiModel<HttpStatusCode>> DeleteBoard(Guid id);      
    }
}
