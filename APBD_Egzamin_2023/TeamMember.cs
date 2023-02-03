using System;
using System.Collections.Generic;

namespace ProjectsAndTasks;

public partial class TeamMember
{
    public int IdTeamMember { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Task> TaskIdAssignedToNavigations { get; } = new List<Task>();

    public virtual ICollection<Task> TaskIdCreatorNavigations { get; } = new List<Task>();
}
