﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserRegistrationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        // Add additional properties as needed, such as 'ConfirmPassword', 'UserName', etc.
    }
}
