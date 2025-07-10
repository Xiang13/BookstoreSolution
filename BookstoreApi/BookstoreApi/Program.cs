
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


// 註冊資料庫服務
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// 自動註冊 Services 和 Repositories
builder.Services.AddServicesByConvention("BookstoreApi.Services");
builder.Services.AddServicesByConvention("BookstoreApi.Infrastructures");
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<MemberService>();

// 註冊 CORS 規則
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

// 自訂 JWT 產生服務
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

// 啟用 CORS
app.UseCors("AllowAll");

// 解析 Token 建立 HttpContext.User
app.UseAuthentication();
// 根據使用者的角色（Role）或身份來判斷是否允許進入某些 API。
app.UseAuthorization();

app.MapControllers();

app.Run();
