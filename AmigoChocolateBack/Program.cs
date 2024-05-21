using AmigoChocolateBack.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AmigoChocolateBackContext>(options =>
    options.UseSqlServer(@"Data Source=10.107.176.41,1434;
                            User ID=RA043411;
                            Password=043411;
                            TrustServerCertificate=true"));

var urlsLiberadas = "urlsLiberadas";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: urlsLiberadas,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:8081");
                          builder.AllowAnyOrigin();
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                      });
});


//201.62.57.93,1434;
//10.107.176.41



// Adicione o serviço do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AmigoChocolateBack", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Habilitar middleware para servir o Swagger gerado como um endpoint JSON
    app.UseSwagger();
    // Habilitar middleware para servir o swagger-ui (HTML, JS, CSS, etc.),
    // especificando o endpoint Swagger JSON
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppMobile V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.UseCors(urlsLiberadas);

app.Run();
