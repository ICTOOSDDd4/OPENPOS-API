using OPENPOS_API;
using System.Globalization;
using OPENPOS_API;
using OPENPOS_API.Middlewares;

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
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseMiddleware<AuthorizationMiddleware>();

Events.Initialize(app);

app.MapControllers();

app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthorization();


app.Run();
