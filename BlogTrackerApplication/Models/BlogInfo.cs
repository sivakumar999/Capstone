using System;
using System.Collections.Generic;

namespace BlogTrackerApplication.Models;

public partial class BlogInfo
{
    public int BlogId { get; set; }

    public string? Title { get; set; }

    public string? Subject { get; set; }

    public DateTime? DateOfCreation { get; set; }

    public string? BlogUrl { get; set; }

    public string? EmpEmailId { get; set; }
}
