using System;

namespace API.Models;

public class ReservationDetails
{
    public int ReservationId { get; set; }
    public string DoctorName { get; set; }
    public string DoctorSurname { get; set; }
    public DateOnly ReservationDate { get; set; }
    public TimeOnly ReservationHour { get; set; }
    public string ServiceType { get; set; }
    public string Specialization { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
}