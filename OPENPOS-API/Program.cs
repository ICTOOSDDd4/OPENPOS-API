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
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.Use(async (context, next) =>
{
    AuthorizationMiddleware authorization = new AuthorizationMiddleware(next);
    await authorization.Invoke(context, builder);
});

app.MapControllers();

app.MapHub<EventHub>("/event_hub");

app.UseHttpsRedirection();

app.UseAuthorization();


app.Run();
