using System;

namespace API.DTO;

public class ReservationRequest
{
    public int UserId { get; set; }
    public int DoctorId { get; set; }
    public DateOnly ReservationDate { get; set; }
    public TimeOnly ReservationHour { get; set; }
    public string ServiceType { get; set; }
}