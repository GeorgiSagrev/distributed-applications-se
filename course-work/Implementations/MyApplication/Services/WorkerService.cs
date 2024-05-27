namespace MyApplication.Services
{
    public class WorkerService : BaseService
    {
        public static WorkerService Instance { get; } = new WorkerService();

        public WorkerService() : base("ManageWorkers")
        {

        }
    }
}
