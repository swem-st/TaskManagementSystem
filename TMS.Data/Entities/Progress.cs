using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Entities
{
    public class Progress
    {
        public Guid Id { get; set; }
        public string ProgressName { get; set; }
        public ICollection<TaskModel> TaskModels { set; get; }
    }
}
