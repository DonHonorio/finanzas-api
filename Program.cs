using Microsoft.EntityFrameworkCore;
using FinanzasApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Agrega el servicio CORS con una política (aquí se permite cualquier origen, pero en producción se recomienda especificar)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://finanzas-frontend-gaug.onrender.com") // Permite solicitudes desde el origen de tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Agrega el servicio de Controllers
builder.Services.AddControllers();

// Agrega OpenAPI si lo deseas
builder.Services.AddOpenApi();

// Obt�n la cadena de conexi�n desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura el DbContext para usar MySQL con el conector oficial
builder.Services.AddDbContext<FinanzasContext>(options =>
    options.UseMySQL(connectionString));

var app = builder.Build();

// Habilita el middleware de CORS usando la política definida
app.UseCors("AllowFrontend");

// Configura el pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
// Mapea los endpoints de los controllers
app.MapControllers();


app.Run();
