using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AngularApp32.Server.Data; // Upewnij siê, ¿e przestrzeñ nazw jest poprawna

var builder = WebApplication.CreateBuilder(args);

// Skonfiguruj DbContext do u¿ywania SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Skonfiguruj Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Dodaj kontrolery i inne us³ugi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Skonfiguruj potok HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Upewnij siê, ¿e uwierzytelnianie jest w³¹czone
app.UseAuthorization();  // Upewnij siê, ¿e autoryzacja jest w³¹czona

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
