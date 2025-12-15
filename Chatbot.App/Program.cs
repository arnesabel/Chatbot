using Chatbot.App.Areas.Identity;
using Chatbot.App.Hubs;
using Chatbot.Core.Constants;
using Chatbot.Core.Entities;
using Chatbot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString(AppDbContext.CONNECTION_STRING_NAME)
    ?? throw new InvalidOperationException($"Connection string '{AppDbContext.CONNECTION_STRING_NAME}' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services
    .AddDefaultIdentity<User>()
    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddScoped<TokenProvider>();

// Add services to the container.
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

app.Run();