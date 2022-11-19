using Microsoft.EntityFrameworkCore;
using WitcherProject.BL.QueryObjects;
using WitcherProject.DAL;
using WitcherProject.DAL.Models;
using WitcherProject.Infrastructure.EFCore.Query;
using WitcherProject.Infrastructure.EFCore.Repository;
using WitcherProject.Infrastructure.EFCore.UnitOfWork;
using WitcherProject.Infrastructure.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<KaerMorhenDBContext>((options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KaerMorhenDatabase")).UseLazyLoadingProxies()));

builder.Services.AddTransient(typeof(IQuery<>), typeof(EFQuery<>));
builder.Services.AddScoped<IContractRequestQueryObject, ContractRequestQueryObject>();
builder.Services.AddScoped<IContractQueryObject, ContractQueryObject>();

builder.Services.AddScoped<IUnitOfWorkAuthentication, UnitOfWorkAuthentication>();
builder.Services.AddScoped<IUnitOfWorkContracts, UnitOfWorkContracts>();
builder.Services.AddScoped<IUnitOfWorkPersonalData, UnitOfWorkPersonalData>();

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