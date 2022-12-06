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
=======
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.MapControllers();

// Temporary fix
app.Use(async (context, next) =>
{
    AuthorizationMiddleware authorization = new AuthorizationMiddleware(next);
    if (context.Request.Path == "/event_hub" ||
        context.Request.Path == "/api/order")
    {
        await authorization.Invoke(context, builder);
    }
    else
    {
        next(context);
    }
});

app.MapHub<EventHub>("/event_hub");

app.UseHttpsRedirection();

app.UseAuthorization();


app.Run();
