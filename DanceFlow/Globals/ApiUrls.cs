namespace DanceFlow {
    public class ApiUrls {
        private ApiUrls()
        {
        }

        private static string ApiBaseUrl = "http://localhost:41901/api/";
        public static string AllPendingApplications = $"{ApiBaseUrl}applications/all";
        public static string AllocateApplications = $"{ApiBaseUrl}applications/allocate";
        public static string AllAllocatedTasks = $"{ApiBaseUrl}tasks";
        public static string ExamineName = $"{ApiBaseUrl}ex/name";
        public static string FinishNameSearchExamination = $"{ApiBaseUrl}ex/name/f";
        public static string AllNamesExamination = $"{ApiBaseUrl}ex/name";
        public static string FinishPvtApplicationExamination = $"{ApiBaseUrl}ex/pvt/f";
    }
}