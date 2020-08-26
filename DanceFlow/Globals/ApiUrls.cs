namespace DanceFlow {
    public class ApiUrls {
        private ApiUrls()
        {
        }

        private static string ApiBaseUrl = "http://localhost:41901/";
        public static string AllPendingApplications = $"{ApiBaseUrl}applications/all";
        public static string AllocateApplications = $"{ApiBaseUrl}applications/allocate";
        public static string AllAllocatedTasks = $"{ApiBaseUrl}applications/allocated";
    }
}