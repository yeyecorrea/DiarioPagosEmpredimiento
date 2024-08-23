using DiarioPagosApp.Models;
using DiarioPagosApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


// Politica de Authenticacion global
var policyAuthenticatedUsers = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();


// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(policyAuthenticatedUsers));
});

var defaultCulture = new System.Globalization.CultureInfo("es-CO");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;


//SERVICES

//Customers 
builder.Services.AddTransient<IRepositoryCustomer, RepositoryCustomer>();


//Products
builder.Services.AddTransient<IRepositoryProduct, RepositoryProduct>();

//Sales
builder.Services.AddTransient<IRepositorySale, RepositorySale>();

//PaymentsStatus
builder.Services.AddTransient<IRepositoryPaymentStatus, RepositoryPaymentStatus>();

//SaleDetails
builder.Services.AddTransient<IRepositorySaleDetail, RepositorySaleDetail>();


builder.Services.AddHttpContextAccessor();

//Users
builder.Services.AddTransient<IRepositoryUser, RepositoryUser>();

//IStore
builder.Services.AddTransient<IUserStore<User>, UserStore>();

//SignManager
builder.Services.AddTransient<SignInManager<User>>();

// IdentityCore
builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});


// Authrnticacion
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = @"/User/UserLogin";
});

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
