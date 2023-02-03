using System;
using System.Collections.Generic;

namespace ProjectsAndTasks;

public partial class Project
{
    public int IdProject { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Deadline { get; set; }

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
