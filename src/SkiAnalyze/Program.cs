using SkiAnalyze.Application;
using SkiAnalyze.Infrastructure;
using SkiAnalyze.Core;
using SkiAnalyze.Data;
using SkiAnalyze.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
var osmPath = builder.Configuration.GetValue<string>("OsmDataPath");

if (connectionString == null) throw new Exception("Missing connection string in appsettings");
if (osmPath == null) throw new Exception("Missing osm path in appsettings");

builder.Services.AddCors(x =>
{
    x.AddPolicy("MyPolicy", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services.AddCore();
builder.Services.AddInfrastructure(connectionString, osmPath);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
builder.Services.AddSpaStaticFiles(config => { config.RootPath = "ClientApp/dist"; });

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("MyPolicy");
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
    }
});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.EnsureCreatedAsync();

    var alpsNe = new Coordinate
    {
        Latitude = app.Configuration.GetValue<float>("OsmBounds:NorthEast:Latitude"),
        Longitude = app.Configuration.GetValue<float>("OsmBounds:NorthEast:Longitude"),
    };
    var alpsSw = new Coordinate
    {
        Latitude = app.Configuration.GetValue<float>("OsmBounds:SouthWest:Latitude"),
        Longitude = app.Configuration.GetValue<float>("OsmBounds:SouthWest:Longitude"),
    };

    var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
    await initializer.LoadInitialData(app.Configuration, alpsNe, alpsSw);
}

app.Run();