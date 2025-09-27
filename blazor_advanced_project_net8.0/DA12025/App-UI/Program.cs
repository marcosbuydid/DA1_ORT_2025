using App_UI.Components;
using DataAccess;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IMovieRepository, InMemoryDatabase>();
builder.Services.AddSingleton<IUserRepository, InMemoryDatabase>();
//builder.Services.AddScoped<IMovieRepository, MovieRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.AddDbContextFactory<AppDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        providerOptions => providerOptions.EnableRetryOnFailure())
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();