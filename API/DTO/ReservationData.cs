using System.Collections.Generic;
using API.Models;

namespace API.DTO;

public class ReservationData
{
    public string DoctorId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Specialization { get; set; }
    public string? Avatar { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
    public List<ServiceModel> Services { get; set; }
}