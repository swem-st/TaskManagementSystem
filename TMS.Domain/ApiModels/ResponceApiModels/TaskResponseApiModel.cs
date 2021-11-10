using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Entities;

namespace TMS.Domain.ApiModels.ResponceApiModels
{
    public class TaskResponseApiModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public ProgressResponseApiModel Progress { get; set; }
        public BoardResponseApiModel Board { get; set; }
        public IEnumerable<SubTaskResponseApiModel> SubTaskModels { get; set; }
    }
}
