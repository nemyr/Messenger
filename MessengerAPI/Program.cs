using DAL;
using MessengerAPI.Filters;
using MessengerAPI.Middlewares;
using MessengerAPI.Services.Extentions;
using MessengerAPI.Services.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddConfigurationOptions(builder);
builder.Services.AddJwtAuthentification();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<ApplicationContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("MessengerAPI"))
    );
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseJwtMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
