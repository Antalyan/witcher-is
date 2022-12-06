using Microsoft.EntityFrameworkCore;
using WitcherProject.BL.QueryObjects;
using WitcherProject.BL.Services.Implementations;
using WitcherProject.BL.Services.Interfaces;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Query;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;
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

// builder.Services.AddScoped<IUnitOfWorkAuthentication, UnitOfWorkAuthentication>();
// builder.Services.AddScoped<IUnitOfWorkContracts, UnitOfWorkContracts>();
// builder.Services.AddScoped<IUnitOfWorkPersonalData, UnitOfWorkPersonalData>();

builder.Services.AddTransient<IContractorService, ContractorService>();
builder.Services.AddTransient<IContractService, ContractService>();
// builder.Services.AddScoped<IContractRequestService, ContractRequestService>();
builder.Services.AddTransient<IPersonService, PersonService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();