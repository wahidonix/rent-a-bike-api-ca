using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Bicycle
{
    public int BicycleId { get; set; }
    public string IdentificationNumber { get; set; } = string.Empty;
    public string BicycleType { get; set; } = string.Empty;
    public string LockCode { get; set; } = string.Empty;
}
