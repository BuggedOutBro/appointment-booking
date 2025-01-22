using System.Data;
using appointment_booking.DBExecuter;
using appointment_booking.DBExecuter.Interface;
using appointment_booking.Repositories;
using appointment_booking.Repositories.Interface;
using appointment_booking.Services;
using appointment_booking.Services.Interface;
using appointment_booking.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverterWithMilliseconds());
    });
// Add FluentValidation    
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(CalendarRequestValidator).Assembly);

// Add DbContext
builder.Services.AddScoped<IDbConnection>(sp => 
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories
builder.Services.AddScoped<IDbQueryExecutor, DapperQueryExecutor>();
builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();

// Add Services
builder.Services.AddScoped<ICalendarService, CalendarService>();

// Add logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
//app.UseHttpsRedirection();

app.Run();

