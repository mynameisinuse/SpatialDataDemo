using Microsoft.EntityFrameworkCore;
using SpatialDataDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGeoCoordinatesService, GeoCoordinatesService>();



//EF Core
builder.Services.AddDbContext<LocationsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"),
        x =>
        {
            x.UseNetTopologySuite();
            x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
