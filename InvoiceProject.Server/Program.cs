
using Domain.Shared.Interfaces;
using Infrastrcture.Repositories;
using Infrastructure.DatabaseContexts;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Infrastrcture
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();

// Application
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
