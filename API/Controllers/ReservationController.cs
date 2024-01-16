using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly DataContext _context;
    
    public ReservationController(DataContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public Task<ActionResult<ReservationData>> GetReservationData([FromQuery] string doctorId)
    {
        var doctorData = _context.Users.FirstOrDefault(u => u.Id.ToString() == doctorId);
        var doctorServices = doctorData.DoctorData.Service.Select(service => new ServiceModel { ServiceName = service.Name, ServicePrice = service.Price }).ToList();
        return Task.FromResult<ActionResult<ReservationData>>(new ReservationData
        {
            DoctorId = doctorData.Id.ToString(),
            Name = doctorData.Name,
            Surname = doctorData.Surname,
            Specialization = doctorData.DoctorData.Specialization,
            Avatar = doctorData.Avatar,
            City = doctorData.DoctorData.Address.City,
            Street = doctorData.DoctorData.Address.Street,
            BuildingNumber = doctorData.DoctorData.Address.BuildingNumber,
            Services = doctorServices
        });
    }
    
    [HttpPost]
    public async Task MakeReservation([FromBody] ReservationRequest body)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == body.UserId);
        user?.Reservations.Add(new Reservation
        {
            DoctorId = body.DoctorId,
            ReservationDate = body.ReservationDate,
            ReservationHour = body.ReservationHour,
            ServiceType = body.ServiceType
        });
        await _context.SaveChangesAsync();
    }
    
    [HttpPost("remove")]
    public async Task RemoveReservation([FromBody] int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }
}