using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Enums;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    
    public UserController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user;
    }
    
    [HttpPost("login")]
    public Task<ActionResult<string>> Login([FromBody] LoginRequest body)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == body.Username && u.Password == body.Password);
        if (user == null) throw new AuthenticationException("User not found");
        return Task.FromResult<ActionResult<string>>(user.Id.ToString());
    }
    
    [HttpPost("register")]
    public async Task Register([FromBody] RegisterRequest body)
    {
        _context.Users.Add(new User
        {
            Username = body.Username,
            Password = body.Password,
            Email = body.Email,
            Role = Role.User,
            Name = body.Name,
            Surname = body.Surname
        });
        await _context.SaveChangesAsync();
    }
    
    [HttpPost("changePassword")]
    public async Task ChangePassword([FromBody] ChangePasswordRequest body)
    {
        var user = await _context.Users.FindAsync(body.UserId);
        user.Password = body.Password;
        await _context.SaveChangesAsync();
    }
    
    [HttpPost("changeAvatar")]
    public async Task ChangeAvatar([FromBody] ChangeAvatarRequest body)
    {
        var user = await _context.Users.FindAsync(body.UserId);
        user.Avatar = body.Avatar;
        await _context.SaveChangesAsync();
    }
    
    [HttpPost("changeAddress")]
    public async Task ChangeAddress([FromBody] ChangeAddressRequest body)
    {
        var user = await _context.Users.FindAsync(body.UserId);
        user.DoctorData.Address.City = body.City;
        user.DoctorData.Address.Street = body.Street;
        user.DoctorData.Address.BuildingNumber = body.BuildingNumber;
        await _context.SaveChangesAsync();
    }
    
    [HttpPost("changeServices")]
    public async Task ChangeServices([FromBody] ChangeServicesRequest body)
    {
        var user = await _context.Users.FindAsync(body.UserId);
        user.DoctorData.Service = body.Services.Select(s => new Service{Name = s.ServiceName, Price = s.ServicePrice}).ToList();
        await _context.SaveChangesAsync();
    }
    
    [HttpPost("changeExperience")]
    public async Task ChangeExpierience([FromBody] ChangeExpierienceRequest body)
    {
        var user = await _context.Users.FindAsync(body.UserId);
        user.DoctorData.Experience = body.Expierience;
        await _context.SaveChangesAsync();
    }
    
    [HttpPost("changeAvailability")]
    public async Task ChangeAvailability([FromBody] ChangeAvailabilityRequest body)
    {
        var user = await _context.Users.FindAsync(body.UserId);
        user.DoctorData.Days = body.Days;
        user.DoctorData.Hours = body.Hours;
        await _context.SaveChangesAsync();
    }
    
    [HttpPost("profile")]
    public async Task<ActionResult<ProfileResponse>> Profile([FromBody] int id)
    {
        var user = await _context.Users.FindAsync(id);
        var response = new ProfileResponse
        {
            Name = user.Name,
            Surname = user.Surname,
            Role = user.Role,
            Specialization = user.DoctorData?.Specialization,
            Expirience = user.DoctorData?.Experience,
            Avatar = user.Avatar,
            City = user.DoctorData?.Address.City,
            Street = user.DoctorData?.Address.Street,
            BuildingNumber = user.DoctorData?.Address.BuildingNumber,
            Services = user.DoctorData?.Service.Select(s => new ServiceModel { ServiceName = s.Name, ServicePrice = s.Price }).ToList(),
            Days = user.DoctorData?.Days,
            Hours = user.DoctorData?.Hours,
            AssociatedReservations = user.Reservations.Select(r => new ReservationDetails
            {
                ReservationId = r.Id,
                DoctorName = _context.Users.Find(r.DoctorId).Name,
                DoctorSurname = _context.Users.Find(r.DoctorId).Surname,
                Specialization = _context.Users.Find(r.DoctorId).DoctorData.Specialization,
                ReservationDate = r.ReservationDate,
                ReservationHour = r.ReservationHour,
                ServiceType = r.ServiceType,
                City = _context.Users.Find(r.DoctorId).DoctorData.Address.City,
                Street = _context.Users.Find(r.DoctorId).DoctorData.Address.Street,
                BuildingNumber = _context.Users.Find(r.DoctorId).DoctorData.Address.BuildingNumber,
            }).ToList()
        };
        return response;
    }
    
    [HttpGet("home")]
    public Task<ActionResult<IEnumerable<HomeData>>> GetHomeData(string? phrase, string? city)
    {
        var users = _context.Users.Where(u => u.Role == Role.Doctor).ToList();
        if (phrase != null)
        {
            users = users.Where(u => u.Name == phrase || u.Surname == phrase || u.DoctorData.Specialization == phrase).ToList();
        }
        if (city != null)
        {
            users = users.Where(u => u.DoctorData.Address.City == city).ToList();
        }
        var reservations = _context.Reservations.ToList();
        return Task.FromResult<ActionResult<IEnumerable<HomeData>>>(users.Select(user => new HomeData
            {
                DoctorId = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                Specialization = user.DoctorData.Specialization,
                Avatar = user.Avatar,
                City = user.DoctorData.Address.City,
                Street = user.DoctorData.Address.Street,
                BuildingNumber = user.DoctorData.Address.BuildingNumber,
                ServiceName = user.DoctorData.Service.ToList()[0].Name,
                ServicePrice = user.DoctorData.Service.ToList()[0].Price,
                Days = user.DoctorData.Days,
                Hours = user.DoctorData.Hours,
                AssociatedReservations = reservations.Where(d => d.DoctorId == user.Id).ToList()
            })
            .ToList());
    }
}