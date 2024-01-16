using System;

namespace API.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public DateOnly ReservationDate { get; set; }
    public TimeOnly ReservationHour { get; set; }
    public string ServiceType { get; set; }
}