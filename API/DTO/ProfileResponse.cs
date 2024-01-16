using System;
using System.Collections.Generic;
using API.Enums;
using API.Models;

namespace API.DTO;

public class ProfileResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Role Role { get; set; }
    public string? Specialization { get; set; }
    public string? Expirience { get; set; }
    public string? Avatar { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public int? BuildingNumber { get; set; }
    public List<ServiceModel>? Services { get; set; }
    public List<Days>? Days { get; set; }
    public List<TimeOnly>? Hours { get; set; }
    public List<ReservationDetails> AssociatedReservations { get; set; }
}