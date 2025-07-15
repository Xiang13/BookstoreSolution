
using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Extensions.DependencyInjection;
using BookstoreApi.Models.Infrastructures.Repositories.Books;
using BookstoreApi.Services.Auth;
using BookstoreApi.Services.Auth.Interfaces;
using BookstoreApi.Services.Users;
using BookstoreApi.Services.Users.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ���U��Ʈw�A��
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// �۰ʵ��U Services �M Repositories
builder.Services.AddServicesByConvention("BookstoreApi.Services");
builder.Services.AddServicesByConvention("BookstoreApi.Infrastructures");
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MemberService>();

// ���U CORS �W�h
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5173")              
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();

var secret = builder.Configuration["Jwt:Key"]
             ?? throw new InvalidOperationException("Jwt:Key not found in configuration");
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["access_token"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret)
            ),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("MemberOnly", policy => policy.RequireRole("Member", "Admin"));
    options.AddPolicy("GuestAccess", policy => policy.RequireRole("Guest", "Member", "Admin"));
});

// �ۭq JWT ���ͪA��
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// �ҥ� CORS
app.UseCors("AllowAll");

// �ѪR Token �إ� HttpContext.User
app.UseAuthentication();
// �ھڨϥΪ̪�����]Role�^�Ψ����ӧP�_�O�_���\�i�J�Y�� API�C
app.UseAuthorization();

app.MapControllers();

app.Run();
