using DESAFIOKHIPO.DesafioDotNet.Data;
using DESAFIOKHIPO.DesafioDotNet.Repository;
using DESAFIOKHIPO.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("https://localhost:7193",
                                              "http://localhost:5184");
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDBContext>(options =>
{
    /*"server=localhost;database=Loja;user id=raphael;password=teste"*/
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoSQLServer"));
});

builder.Services.AddScoped<IProductsRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
