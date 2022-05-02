using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.
builder.Services.AddControllersWithViews();

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
/*builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection2")
    ));
*/

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaulConnection")
    ));
//builder.Services.AddDefaultIdentity<IdentityUser>()
  //  .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); //replace by UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();//use for take care all the Repository
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); //for runtime compilation
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
//app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
