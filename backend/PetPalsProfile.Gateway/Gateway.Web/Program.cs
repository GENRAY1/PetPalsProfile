using Gateway.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxyServices(builder.Configuration);
builder.Logging.AddConsole();

var app = builder.Build();
app.MapReverseProxy();
app.Run();
