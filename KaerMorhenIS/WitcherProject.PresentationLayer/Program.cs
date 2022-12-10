using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WitcherProject.BL.DTOs;
using WitcherProject.BL.DTOs.Person;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Implementations;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Query;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Infrastructure.Query;
using WitcherProject.PresentationLayer.Model;
using WitcherProject.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<KaerMorhenDBContext>((options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KaerMorhenDatabase"))),   
    ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<KaerMorhenDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("KaerMorhenDatabase")), ServiceLifetime.Transient);

var config = TypeAdapterConfig.GlobalSettings;
config.ForType<Role, RoleDto>().TwoWays()
    .Map(rd => rd.UserRoleDtos, r => r.UserRoles).PreserveReference(true);
config.ForType<Person, PersonCompleteDto>().TwoWays()
    .Map(pcd => pcd.UserRoleDtos, p => p.UserRoles).PreserveReference(true)
    .Map(pcd => pcd.Contracts, p => p.Contracts).PreserveReference(true);


builder.Services.AddTransient(typeof(IQuery<>), typeof(EFQuery<>));
builder.Services.AddTransient<IContractRequestQueryObject, ContractRequestQueryObject>();
builder.Services.AddTransient<IContractQueryObject, ContractQueryObject>();

builder.Services.AddScoped<IUnitOfWorkProvider, EFUnitOfWorkProvider>();

builder.Services.AddTransient<IContractorService, ContractorService>();
builder.Services.AddTransient<IContractService, ContractService>();
// builder.Services.AddScoped<IContractRequestService, ContractRequestService>();
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));

builder.Services.AddAuthorizationCore(
    options =>
    {
        options.AddPolicy(RoleNames.Witcher,
            authBuilder => { authBuilder.RequireRole(RoleNames.Witcher); });
        options.AddPolicy(RoleNames.UserManager,
            authBuilder => { authBuilder.RequireRole(RoleNames.UserManager);});
        options.AddPolicy(RoleNames.Admin,
            authBuilder => { authBuilder.RequireRole(RoleNames.Admin);});
        options.AddPolicy(RoleNames.ContractManager,
            authBuilder => { authBuilder.RequireRole(RoleNames.ContractManager);});
    });

builder.Services.AddIdentity<Person, Role>()
    .AddEntityFrameworkStores<KaerMorhenDBContext>()
    .AddDefaultTokenProviders();  

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;
    
    // User settings
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseMiddleware<BlazorCookieLoginMiddleware<Person>>();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();