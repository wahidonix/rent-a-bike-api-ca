﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(ApplicationUser user);
}
