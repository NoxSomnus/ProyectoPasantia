﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Responses
{
    public class AddInscriptionsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public List<string>? Cedulas { get; set; }
    }
}
