namespace KarateTournamentSoftware.Models {
    public class ErrorViewModel {
        public string? RequestID { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? ExceptionFull { get; set; }

        public bool ShowRequestID => !string.IsNullOrEmpty(RequestID);
        public bool ShowExceptionMessage => !string.IsNullOrEmpty(ExceptionMessage);
    }
}
