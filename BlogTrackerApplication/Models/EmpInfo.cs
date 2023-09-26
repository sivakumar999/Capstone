using System;
using System.Collections.Generic;

namespace BlogTrackerApplication.Models;

public partial class EmpInfo
{
    public int Id { get; set; }

    public string? EmailId { get; set; }

    public string? Name { get; set; }

    public DateTime? DateOfJoining { get; set; }

    public int? PassCode { get; set; }
}
