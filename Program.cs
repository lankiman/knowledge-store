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
builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<ELearningDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    app.UseHsts();
    app.UseHttpsRedirection();
}

//db context
//
// var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ELearningDbContext>();

// context.Database.EnsureDeleted();

// context.Database.Migrate();

//Initialize Identity User Roles
//
// var roleInitializer = new RoleInitializerService(app.Services);
//
// await roleInitializer.InitializeRoles();
//
// //Initialize Default Admin User
//
// var adminInitializer = new AdminUserInitializerService(app.Services, builder.Configuration);
//
// await adminInitializer.InitializeAdmin();

app.UseStaticFiles();

app.MapGet("/environment", async context =>
{
    var envName = app.Environment.EnvironmentName;
    await context.Response.WriteAsync(envName);
});


app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "splendid/{action}",
    defaults: new { controller = "Admin" });

app.Run();