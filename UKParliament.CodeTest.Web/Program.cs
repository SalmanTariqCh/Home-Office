using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.DataContext;
using UKParliament.CodeTest.Services.DataContext;
using UKParliament.CodeTest.Services.Interfaces.EFCoreSql;
using UKParliament.CodeTest.Services.Interfaces.InMemory;

var builder = WebApplication.CreateBuilder(args);
var constr = builder.Configuration.GetConnectionString("UserDbConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPersonService, PersonServiceInMemory>();
builder.Services.AddScoped<IPersonServiceEFCore, PersonServiceEFCore>();

//InMemory Repositories
builder.Services.AddDbContext<PersonManagerContext>(op => op.UseInMemoryDatabase("PersonManager"));

//EFCore Repositories
builder.Services.AddDbContext<EFCorePersonContext>(options =>
    options.UseSqlServer(constr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
