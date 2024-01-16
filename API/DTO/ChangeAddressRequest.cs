namespace API.DTO;

public class ChangeAddressRequest
{
    public int UserId { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
}