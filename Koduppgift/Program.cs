using Koduppgift.Data;
using Koduppgift.Interfaces;
using Koduppgift.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("johannaDb");
	options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();

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
