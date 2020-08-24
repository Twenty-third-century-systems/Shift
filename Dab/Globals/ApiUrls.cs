namespace Dab.Globals {
    public class ApiUrls {
        private ApiUrls()
        {
        }

        private static string ApiBaseUrl = "https://localhost:44312/api/";
        public static readonly string NameSearchDefaultsUrl = $"{ApiBaseUrl}name/defaults";
        public static readonly string SubmitNameSearchUrl = $"{ApiBaseUrl}name/submit";
    }
}