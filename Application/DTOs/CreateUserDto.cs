﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateUserDto
    {
        public string Email { get; set; }  = string.Empty;
        public string Password { get; set; } = string.Empty;
        // Add other properties as necessary, such as FirstName, LastName, etc.
    }
}