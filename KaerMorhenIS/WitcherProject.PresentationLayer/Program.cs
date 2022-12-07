using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Implementations;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Query;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWorkProvider;
using WitcherProject.Infrastructure.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// builder.Services.AddDbContext<KaerMorhenDBContext>((options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("KaerMorhenDatabase")).UseLazyLoadingProxies()),   
//     ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<KaerMorhenDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("KaerMorhenDatabase")));

builder.Services.AddTransient(typeof(IQuery<>), typeof(EFQuery<>));
builder.Services.AddTransient<IContractRequestQueryObject, ContractRequestQueryObject>();
builder.Services.AddTransient<IContractQueryObject, ContractQueryObject>();

builder.Services.AddScoped<IUnitOfWorkProvider, EFUnitOfWorkProvider>();

builder.Services.AddTransient<IContractorService, ContractorService>();
builder.Services.AddTransient<IContractService, ContractService>();
// builder.Services.AddScoped<IContractRequestService, ContractRequestService>();
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));

builder.Services.AddAuthorizationCore();
builder.Services.AddIdentity<Person, IdentityRole<int>>()
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseMiddleware<BlazorCookieLoginMiddleware<Person>>();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();