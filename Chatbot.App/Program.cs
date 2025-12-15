using Chatbot.App.Areas.Identity;
using Chatbot.App.Hubs;
using Chatbot.App.Services;
using Chatbot.Core.Constants;
using Chatbot.Core.Entities;
using Chatbot.Core.Interface;
using Chatbot.Core.Services;
using Chatbot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAppDbContext(builder.Configuration);

builder.Services
    .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<TokenProvider>();
builder.Services.AddSingleton<IBotCommandService, BotCommandService>();
builder.Services.AddSingleton<IBotCommandRequestService, BotCommandRequestService>();
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddHostedService<BotResponseBackgroundService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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
app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<ChatRoomHub>(HubConstants.CHAT_ROOM_HUB);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 
}

app.Run();