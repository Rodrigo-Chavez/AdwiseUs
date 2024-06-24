namespace adwiseus.Models
{
    public class EmailSendingResult
    {
        public string InputName { get; set; }
        public string InputEmail { get; set; }
        public string InputMessage { get; set; }
        public bool Success { get; set; }
        public string Error {  get; set; }
    }
}
