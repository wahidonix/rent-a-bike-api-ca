using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserUpdateDto
    {
        public int Credits { get; set; }
        // Include other properties that might be updatable, like 'PhoneNumber', 'FirstName', 'LastName', etc.
    }

}
