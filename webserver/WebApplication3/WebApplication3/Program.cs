using WebApplication3.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Service;
using WebApplication3.Service.Remote;
using WebApplication3.Service.TempHum;
using WebApplication3.Service.FireAlarm;
using WebApplication3.Service.StatusBulb;
using WebApplication3.Service.StatusSpeak;
using WebApplication3.Service.StatusAir;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<SmartHomeDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("SmartHomeDatabase"));
        });

        builder.Services.AddTransient<IRemoteService, RemoteService>();
        builder.Services.AddTransient<ITempHumService, TempHumService>();
        builder.Services.AddTransient<IFireAlarmService, FireAlarmService>();
        builder.Services.AddTransient<IStatusBulbService, StatusBulbService>();
        builder.Services.AddTransient<IStatusSpeakService, StatusSpeakService>();
        builder.Services.AddTransient<IStatusAirService, StatusAirService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}