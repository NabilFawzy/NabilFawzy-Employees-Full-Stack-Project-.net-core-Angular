using API.HelpClasses;
using Microsoft.EntityFrameworkCore;
using OnionArch.Data.Entities.Identity;
using OnionArch.Data.Interfaces;
using OnionArch.Repository;
using OnionArch.Repository.Data;
using OnionArch.Repository.Data.DataSeed;
using OnionArch.Repository.Identity;
using OnionArch.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OnionArch.Repository.Services;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;

//ADD Scoped

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

//Add DbContext
builder.Services.AddDbContext<EmployeeContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var nBuilder = builder.Services.AddIdentityCore<AppUser>();
nBuilder = new IdentityBuilder(nBuilder.UserType, nBuilder.Services);
nBuilder.AddEntityFrameworkStores<AppIdentityDbContext>();
nBuilder.AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:key"])),
            ValidIssuer = Configuration["Token:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });


builder.Services.AddSwaggerGen();


var app = builder.Build();



    //Database Migration Update
    //
    using (var scope = app.Services.CreateScope())
    {
        var scopeService = scope.ServiceProvider;
        try
        {
           var mContext=scopeService.GetRequiredService<EmployeeContext>();
           await mContext.Database.MigrateAsync();
        await SystemDataSeed.Seed(mContext, Configuration);


        using (var scopeN = app.Services.CreateScope())
        {
            //Resolve ASP .NET Core Identity with DI help
            var userManager = (UserManager<AppUser>)scopeN.ServiceProvider.GetService(typeof(UserManager<AppUser>));
            var identityContext = (AppIdentityDbContext)scopeN.ServiceProvider.GetService(typeof(AppIdentityDbContext)); 
           
            await identityContext.Database.MigrateAsync();
            await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
        }

        




        }
        catch (Exception err)
        {

        }
    }
app.UseCors(builder => builder
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader()
         );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseCors();




app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
