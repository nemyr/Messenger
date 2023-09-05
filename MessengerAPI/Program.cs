using DAL;
using MessengerAPI.Filters;
using MessengerAPI.Middlewares;
using MessengerAPI.ModelBinders;
using MessengerAPI.Services.Extentions;
using MessengerAPI.Services.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddConfigurationOptions(builder);
builder.Services.AddJwtAuthentification();

builder.Services.AddControllers(options =>
{
    //todo: add model binder for user account
    options.ModelBinderProviders.Insert(0, new UserModelBinderProvider());
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

//todo: decide if it is necessary
//app.UseJwtMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.UseUserDataMiddleware();
app.MapControllers();

app.Run();
