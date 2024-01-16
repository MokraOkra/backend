using System;
using System.Collections.Generic;
using API.Entities;
using API.Enums;

namespace API.DTO;

public class HomeData
{
    public string DoctorId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Specialization { get; set; }
    public string? Avatar { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
    public string ServiceName { get; set; }
    public int ServicePrice { get; set; }
    public List<Days> Days { get; set; }
    public List<TimeOnly> Hours { get; set; }
    public ICollection<Reservation> AssociatedReservations { get; set; }
}