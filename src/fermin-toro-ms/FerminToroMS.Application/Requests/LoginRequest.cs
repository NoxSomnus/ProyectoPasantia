﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerminToroMS.Application.Requests
{
    public class LoginRequest
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
