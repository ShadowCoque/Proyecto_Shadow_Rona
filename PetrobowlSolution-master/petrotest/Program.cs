using Application.Contracts.Identity;
using Application.Persistence;
using Domain;
using Infraestructure;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using Infraestructure.Services.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using petrotest;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfraestructureServices(builder.Configuration);

builder.Services.AddDbContext<PetrobowlDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"),
    b => b.MigrationsAssembly(typeof(PetrobowlDbContext).Assembly.FullName)
    )
);


// Add services to the container.
builder.Services.AddControllersWithViews(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<Usuario>();
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();

identityBuilder.AddEntityFrameworkStores<PetrobowlDbContext>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
})
.AddCookie(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
})
.AddCookie("Identity.External")
.AddCookie("Identity.TwoFactorUserId");

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});

//Interfaces Propias
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddSignalR();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapHub<TorneoHub>("/torneo");
});


using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<PetrobowlDbContext>();
        var usuarioManager = service.GetRequiredService<UserManager<Usuario>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        await context.Database.MigrateAsync();

        await PetrobowlDbContextData.LoadDataAsync(context, usuarioManager, roleManager, loggerFactory);

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Error en la migration");
    }
}

app.Run();
