using Autofac.Extensions.DependencyInjection;
using SkiAnalyze.Infrastructure;
using Microsoft.OpenApi.Models;
using Ardalis.ListStartupServices;
using Autofac;
using SkiAnalyze.Core;
using SkiAnalyze.Data;
using SkiAnalyze.Util;
using SkiAnalyze;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

string connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
string osmPath = builder.Configuration.GetValue<string>("OsmDataPath");
builder.Services.AddDbContext(connectionString);
builder.Services.AddOsmFile(osmPath);
builder.Services.AddScoped<DataInitializer>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.EnableAnnotations();
});
builder.Services.Configure<ServiceConfig>(config =>
{
    config.Services = new List<ServiceDescriptor>(builder.Services);
});
builder.Services.AddCors(x =>
{
    x.AddPolicy("CorsPolicy", cors =>
    {
        cors.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultCoreModule());
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");
app.MapControllers();
app.UseCookiePolicy();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await context.Database.EnsureCreatedAsync();

var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
await initializer.LoadInitialData();

app.Run();