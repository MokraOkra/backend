using System;
using System.Collections.Generic;
using API.Enums;

namespace API.DTO;

public class ChangeAvailabilityRequest
{
    public int UserId { get; set; }
    public List<Days> Days { get; set; }
    public List<TimeOnly> Hours { get; set; }
}