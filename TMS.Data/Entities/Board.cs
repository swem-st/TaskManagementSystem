using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Entities
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<TaskModel> TasksModels { get; set; }
    }
}
