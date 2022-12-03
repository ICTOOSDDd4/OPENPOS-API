using OPENPOS_API;
using System.Globalization;
using OPENPOS_API.NewFolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    AuthorizationMiddleware authorization = new AuthorizationMiddleware(next);
    authorization.Invoke(context, builder);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<EventHub>("/event_hub");

app.Run();
