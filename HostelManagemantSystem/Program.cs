using HostelManagemantSystem.Data;
using HostelManagemantSystem.HostelServices.IServices;
using HostelManagemantSystem.HostelServices.Services;
using Microsoft.EntityFrameworkCore;

// Replace 'AppContext' with a valid DbContext-derived class.
// Assuming the correct class name is 'ApplicationDbContext', update the code as follows:

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserServices, UsersService>();
builder.Services.AddScoped<ITenentService, TenentService>();
builder.Services.AddScoped<IAdminservices, AdminService>();

builder.Services.AddDbContext<HostelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
