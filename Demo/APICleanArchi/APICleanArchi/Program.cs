using APICleanArchi.Data;
using APICleanArchi.Profiles;
using APICleanArchi.Repositories;
using APICleanArchi.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//register entity framework
builder.Services.AddDbContext<AppDbContext>();

// ensure database is created

// register automapper
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(GraduationProfile));
builder.Services.AddAutoMapper(typeof(GradleProfile));

// register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGraduationServices, GraduationServices>();
builder.Services.AddScoped<IGradleService, GradleService>();

// register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGraduationRepository, GraduationRepository>();
builder.Services.AddScoped<IGradleRepository, GradleRepository>();

builder.Services.AddControllers();
//    .AddJsonOptions(x =>
//{
//    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//    x.JsonSerializerOptions.WriteIndented = true;
//    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//    x.JsonSerializerOptions.PropertyNamingPolicy = null;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//ensure database is create migrate
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    //await context.Database.MigrateAsync();
    await context.Database.EnsureCreatedAsync();
}

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
