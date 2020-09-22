namespace Dab.Globals {
    public class ApiUrls {
        private ApiUrls()
        {
        }

        private static string ApiBaseUrl = "https://localhost:44312/api/";
        public static readonly string NameSearchDefaultsUrl = $"{ApiBaseUrl}name/defaults";
        public static readonly string SubmitNameSearchUrl = $"{ApiBaseUrl}name/submit";
        public static readonly string CheckNameAvailability = $"{ApiBaseUrl}name";
        public static readonly string RegisteredNames = $"{ApiBaseUrl}entity/names";
        public static readonly string NameOnApplication = $"{ApiBaseUrl}entity";
        public static readonly string InitialisePvtApplication = $"{ApiBaseUrl}entity/i";
        public static readonly string SubmitPvtApplicationOffice = $"{ApiBaseUrl}entity/o";
        public static readonly string SubmitPvtApplicationsClauses = $"{ApiBaseUrl}/entity/c";
        public static readonly string SubmitPvtApplicationsObjects = $"{ApiBaseUrl}/entity/ob";
        public static readonly string SubmitPvtApplicationsArticleTable = $"{ApiBaseUrl}/entity/a";
        public static readonly string SubmitPvtApplicationsAmendedArticle = $"{ApiBaseUrl}/entity/am";
        public static readonly string SubmitPvtApplicationShareHoldingPeople = $"{ApiBaseUrl}/entity/p";
        public static readonly string SubmitPvtApplicationShareHoldingEntities = $"{ApiBaseUrl}/entity/e";
        public static readonly string SubmitPvtApplication = $"{ApiBaseUrl}/entity/s";
    }
}