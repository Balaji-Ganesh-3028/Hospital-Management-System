using backend.Middleware;
using BusinessLayer.Implementation;
using BusinessLayer.Interface;
using DataAccessLayer.Implementation;
using DataAccessLayer.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//JWT configuration
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<String>();
var jwtKey = builder.Configuration.GetSection("Jwt:key").Get<String>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});


builder.Services.AddCors(Options =>
{
    Options.AddPolicy("Cors-Policy", builder =>
    {
        // TODO: Replace with your specific frontend origins in production
        builder.WithOrigins(
            "http://localhost:5173",
            "http://192.168.1.6:5173"
        ).AllowAnyHeader().AllowAnyMethod();
    });
});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRegisterDAL, RegisterDAL>();
builder.Services.AddScoped<IRegisterService, RegisterBL>();
builder.Services.AddScoped<IUserProfileBL, UserProfileBL>();
builder.Services.AddScoped<IUserProfileDAL, UserProfileDAL>();
builder.Services.AddScoped<IPatientBL, PatientBL>();
builder.Services.AddScoped<IPatientDAL, PatientDAL>();
builder.Services.AddScoped<IDoctorBL, DoctorBL>();
builder.Services.AddScoped<IDoctorDAL, DoctorDAL>();
builder.Services.AddScoped<IAppointmentBL, AppointmentBL>();
builder.Services.AddScoped<IAppointmentDAL, AppointmentDAL>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Cors-Policy");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();

app.Run();
