using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain.ApiModels.RequestApiModels
{
    public class BoardPostApiModel
    {
        [Required(ErrorMessage = "Name didn't indicate")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "String length have to was from 3 to 50 character")]
        public string Name { get; set; }
    }
}
