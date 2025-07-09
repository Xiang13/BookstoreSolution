
using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Extensions.DependencyInjection;
using BookstoreApi.Models.Infrastructures.Repositories.Books;
using BookstoreApi.Services.Auth;
using BookstoreApi.Services.Auth.Interfaces;
using BookstoreApi.Services.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

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

// 註冊 CORS 規則
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5173")              
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 啟用 CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
