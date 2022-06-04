using BlazorAppServer.Data;
using BlazorAppServer.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Connections;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddRazorPages();
//Standard
//builder.Services.AddServerSideBlazor();

builder.Services.AddServerSideBlazor(
                opt =>
                {
                    //Tempo in cui un circuito disconesso viene lasciato in memoria prima che siano liberate le risorse
                    //Default: 3 minuti
                    opt.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(1);

                    //Numero massimo di circuiti disconnessi tenuti in memoria
                    //Default: 100
                    opt.DisconnectedCircuitMaxRetained = 50;

                    //Tempo massimo che il server attende per una operazione asincrona di una funziona javascript
                    //Default: 1 minuto
                    opt.JSInteropDefaultCallTimeout = TimeSpan.FromSeconds(30);

                }
            );

builder.Services.AddSingleton<ArticoliService>();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<CircuitHandler, CountCircuitHandler>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//app.MapBlazorHub();

app.MapBlazorHub(configureOptions: options =>
{
    options.Transports = HttpTransportType.WebSockets;
});

app.MapFallbackToPage("/_Host");

app.Run();
