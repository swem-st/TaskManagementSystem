﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Domain.ApiModels.ResponceApiModels
{
    public class BoardResponseApiModel
    {
        public Guid Id { get; set; }
       
        public string Name { get; set; }
    }
}
