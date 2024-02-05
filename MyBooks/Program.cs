using Microsoft.EntityFrameworkCore;
using MyBooks.DAL;
using MyBooks.DAL.Interfaces;
using MyBooks.DAL.Repositories;
using MyBooks.Domain.Entity;
using MyBooks.Service.Implementations;
using MyBooks.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IAuthorRepository<AuthorEntity>, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");// звязок з файлом appsettings.json за назвою
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();