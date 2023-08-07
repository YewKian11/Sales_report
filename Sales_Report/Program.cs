using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sales_Report.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Sales_ReportContextConnection") ?? throw new InvalidOperationException("Connection string 'Sales_ReportContextConnection' not found.");

builder.Services.AddDbContext<Sales_ReportContext>
   (options => options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<IdentityUser>()
    //Add DefaultUI
    .AddDefaultUI()
    //DefaultToken
    .AddDefaultTokenProviders()
    //AddRole<IdentityRole>
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Sales_ReportContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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


//For Identity pages
app.MapRazorPages();


app.Run();
