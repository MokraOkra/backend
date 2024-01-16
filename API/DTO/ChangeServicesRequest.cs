using System.Collections.Generic;
using API.Models;

namespace API.DTO;

public class ChangeServicesRequest
{
    public int UserId { get; set; }
    public List<ServiceModel> Services { get; set; }
}