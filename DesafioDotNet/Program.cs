using System.Reflection;
using DESAFIOKHIPO.DesafioDotNet.Data;
using DESAFIOKHIPO.DesafioDotNet.Repository;
using DESAFIOKHIPO.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7193",
                                              "http://localhost:5184")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Desafio .NET Core",
        Description = "Desafio de contratação KHIPO.",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Raphael Gonçalves Silva",
            Url = new Uri("https://github.com/Raphael928/DesafioDotNet/tree/main")
        },
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //Options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
