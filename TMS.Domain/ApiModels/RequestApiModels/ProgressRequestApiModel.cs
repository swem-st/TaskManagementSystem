using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain.ApiModels.RequestApiModels
{
    public class ProgressRequestApiModel
    {
        public Guid Id { get; set; }
        public string ProgressName { get; set; }
    }
}
