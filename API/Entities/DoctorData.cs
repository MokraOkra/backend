using System;
using System.Collections.Generic;
using API.Enums;

namespace API.Entities;

public class DoctorData
{
    public int Id { get; set; }
    public string Specialization { get; set; }
    public string Experience { get; set; }
    public List<Days> Days { get; set; }
    public List<TimeOnly> Hours { get; set; }
    public virtual Address Address { get; set; }
    public virtual ICollection<Service> Service { get; set; }
}