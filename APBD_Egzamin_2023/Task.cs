using System;
using System.Collections.Generic;

namespace ProjectsAndTasks;

public partial class Task
{
    public int IdTask { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Deadline { get; set; }

    public int IdProject { get; set; }

    public int IdTaskType { get; set; }

    public int IdAssignedTo { get; set; }

    public int IdCreator { get; set; }

    public virtual TeamMember IdAssignedToNavigation { get; set; } = null!;

    public virtual TeamMember IdCreatorNavigation { get; set; } = null!;

    public virtual Project IdProjectNavigation { get; set; } = null!;

    public virtual TaskType IdTaskTypeNavigation { get; set; } = null!;
}
