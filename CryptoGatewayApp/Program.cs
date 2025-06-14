using CryptoGatewayApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Register DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080); // required for Railway
});


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddRazorPages(); // Add Razor Pages

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
// Enable middleware
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages(); // Enable Razor routing



app.Run();
