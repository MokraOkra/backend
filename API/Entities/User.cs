using System.Collections.Generic;
using API.Enums;

namespace API.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string Email { get; set; }
    public string? Avatar { get; set; }
    public Role Role { get; set; }
    public virtual DoctorData? DoctorData { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; }
}