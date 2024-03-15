using ChartsServer.Hubs;
using ChartsServer.Models;
using ChartsServer.Subscription;
using ChartsServer.Subscription.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(po => po.AllowCredentials().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(x => true)));

builder.Services.AddSignalR();

builder.Services.AddSingleton<DatabaseSubscription<Personel>>();
builder.Services.AddSingleton<DatabaseSubscription<Sale>>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseCors();

app.UseDatabaseSubscription<DatabaseSubscription<Personel>>("Personels");
app.UseDatabaseSubscription<DatabaseSubscription<Sale>>("Sales");

app.UseRouting();

app.MapHub<ChartHub>("/charthub");

app.Run();
