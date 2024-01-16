namespace API.DTO;

public class ChangePasswordRequest
{
    public int UserId { get; set; }
    public string Password { get; set; }
}