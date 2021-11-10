using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain.ApiModels.RequestApiModels
{
    public class SubTaskCutRequestApiModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}
