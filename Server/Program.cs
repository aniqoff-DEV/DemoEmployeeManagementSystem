using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression(options =>
{
    // Using Response Compression on https
    // Not safe from CRIME and BEACH attacks, better use Server based compression!
    options.EnableForHttps = true;
});

/* 
 * Setting the request compression speed.
 * The stronger the compression, the longer it will take on the server, but the request itself will weigh less.
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.SmallestSize;
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    // Fastest - default
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});
*/

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection is not found"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection!.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
    };
});

builder.Services.AddScoped<IUserAccount, UserAccountRepository>();

#region Repositories
builder.Services.AddScoped<IGenericRepository<GeneralDepartment>, GeneralDepartmentRepository>();
builder.Services.AddScoped<IGenericRepository<Department>, DepartmentRepository>();
builder.Services.AddScoped<IGenericRepository<Branch>, BranchRepository>();

builder.Services.AddScoped<IGenericRepository<Country>, CountryRepository>();
builder.Services.AddScoped<IGenericRepository<City>, CityRepository>();
builder.Services.AddScoped<IGenericRepository<Town>, TownRepository>();

builder.Services.AddScoped<IGenericRepository<Employee>, EmployeeRepository>();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm",
        builder => builder
        .WithOrigins("http://localhost:5172", "https://localhost:7121")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
