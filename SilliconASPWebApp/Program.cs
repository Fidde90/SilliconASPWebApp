using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Helpers.MIddlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SilliconASPWebApp.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDefaultIdentity<AppUserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;

})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();
builder.Services.ConfigureApplicationCookie(x =>
{
    x.Cookie.HttpOnly = true; // förhindrar att någon kan läsa ut cookie informationen (vanligt bland javascript).
    x.LoginPath = "/signin";
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/denied";
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always; // ser till att det alltid går via Https och inte Http.
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    x.SlidingExpiration = true;
});
builder.Services.AddAuthentication().AddFacebook(x =>
{
    x.AppId = "1488473432015630";
    x.AppSecret = "5d3f6ee7f45f1a683421f4361aea5266";
    x.Fields.Add("first_name");
    x.Fields.Add("last_name");
});
builder.Services.AddAuthentication().AddGoogle(g =>
{
    g.ClientId = "115908931583-62p0vbcglk7qqd419eisvnfhqf723rel.apps.googleusercontent.com";
    g.ClientSecret = "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";
});
builder.Services.RegisterRepositories(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "User", "Admin" };

    for(int index = 0; index < roles.Length; index++)
    {
        if(!await roleManager.RoleExistsAsync(roles[index]))
        {
            await roleManager.CreateAsync(new IdentityRole(roles[index]));
        }
    }
}

app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error","?statusCode{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseUserSessionValidation(); // kollar om det finns en användare i databasen, om den har tagits bort ur databasen så ska den automatiskt loggas ut.
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
