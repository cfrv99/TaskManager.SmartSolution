﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Responses;

namespace TaskManager.BLL.Services.Dto
{
    public class LoginRequest : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
