namespace ProjectsAndTasks.Controllers
{
    public class TaskRequest
    {
        public int IdTask { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Deadline { get; set; }

        public int IdProject { get; set; }

        public int IdTaskType { get; set; }

        public int IdAssignedTo { get; set; }

        public int IdCreator { get; set; }

        public TaskType TaskType { get; set; }
    }
}