using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data.Entities
{
    //public enum Progress
    //{
    //    Planned,
    //    inProgress,
    //    Completed
    //}
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        public Guid ProgressId { get; set; }
        public Progress Progress { get; set; }

        public Guid BoardId { get; set; }
        public Board Board { get; set; }
        public  ICollection<SubTaskModel> SubTaskModels { get; set; }
    }
}
