using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Entities;

namespace TMS.Domain.ApiModels.RequestApiModels
{
    public class TaskPostRequestApiModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Progress { get; set; }
        public List<SubTaskCutRequestApiModel> SubTaskModel { get; set; }
    }
}
