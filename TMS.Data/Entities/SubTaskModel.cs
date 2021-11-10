using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Entities
{
    public class SubTaskModel
    {
     
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
     
        public Guid TaskModelId { get; set; }
        public TaskModel TaskModel { get; set; }
    }
}
