namespace AssignmentPro.Models.Auth;

public class RegistrationResponse
{
        public bool Success { get; set; } = true;
        public string UserId { get; set; }
        public List<string> Errors { get; set; } = new();
}
