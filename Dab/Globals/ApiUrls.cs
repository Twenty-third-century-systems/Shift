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
    }
}