using e_learning.Data;
using e_learning.Extensions;
using e_learning.Models;
using e_learning.Services.Default;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Register ELearning Business Logic Services

builder.Services.AddELearningBusinessServices();

// Add services to the container.
builder.Services.AddControllersWithViews();


//Add services for DbContext

builder.Services.AddDbContext<ELearningDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ELearningConnectionString"));
});

//Add Identity Service 
builder.Services.AddIdentity<UserModel, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ELearningDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();


//Initialize Identity User Roles

var roleInitializer = new RoleInitializerService(app.Services);

await roleInitializer.InitializeRoles();

//Initialize Default Admin User

var adminInitializer = new AdminUserInitializerService(app.Services, builder.Configuration);

await adminInitializer.InitializeAdmin();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ELearningDbContext>();

// context.Database.Migrate();

// context.Database.EnsureDeleted();

context.Database.EnsureCreated();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();