using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace KarateTournamentSoftware.Models {
    public class HttpErrorModel : PageContext {
        public string PageTitle { get; private set; }
        public string? StatusCode { get; set; }
        public string StatusName { get; private set; }
        public string? StatusDescription { get; private set; }

        public HttpErrorModel(int? errorCode) {
            if (!errorCode.HasValue)
                errorCode = HttpContext?.Response?.StatusCode;

            PageTitle = $"Error {errorCode}";
            StatusCode = errorCode.ToString();

            if (errorCode == 404) {
                StatusName = "Not found";
                StatusDescription = "The requested page was not found.";
            } else if (errorCode.HasValue) {
                StatusName = ReasonPhrases.GetReasonPhrase(errorCode.Value);
            } else {
                StatusName = "Unknown Error";
            }
        }
    }
}
