
using BookstoreApi.Models.EFModels;
using BookstoreApi.Models.Infrastructures.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// 註冊資料庫服務
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// 自動註冊 Services 和 Repositories（請視你的實際命名空間）
builder.Services.AddServicesByConvention("BookstoreApi.Services");
builder.Services.AddServicesByConvention("BookstoreApi.Infrastructures");

// 註冊 CORS 規則
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyMethod()
              .AllowAnyHeader();
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
