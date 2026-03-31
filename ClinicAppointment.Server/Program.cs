
using ClinicAppointment.Server.Services.AppointmentService;
using ClinicAppointmentProject.Models;
using ClinicAppointmentProject.Services.AvailabilitySlotsService;
using ClinicAppointmentProject.Services.DoctorService;
using ClinicAppointmentProject.Services.PatientService;
using ClinicAppointmentProject.Services.SpecialtyService;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Configuration.AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("appsettings.Development.json", true, true)
    .AddEnvironmentVariables();


var test = builder.Configuration.GetConnectionString("LocalConnection"); 

builder.Services.AddDbContext<ClinicAppointmentDatabaseContext>(
        dbContextOptions => dbContextOptions.UseSqlServer(
            builder.Configuration.GetConnectionString("LocalConnection"))

    );

builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
//builder.Services.AddScoped<IAvailabilitySlotsService, AvailabilitySlotsService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPatientService, PatientService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
