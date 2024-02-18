using CrudNet8MVC.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//Configurar la conexion a la base de datos
builder.Services.AddDbContext<AplicationDBContext>(opciones =>opciones.UseOracle(builder.Configuration.GetConnectionString("ConexionSQL")));

//builder.Services.AddDbContext<AplicationDBContext>(opciones =>opciones.UseSqlite(builder.Configuration.GetConnectionString("ConexionSQLite")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AplicationDBContext>();

    try
    {
        // Intentar realizar una consulta simple a la base de datos
        var firstEntity = dbContext.Contacto.FirstOrDefault(); // Reemplaza "Entity" por el nombre de una tabla existente en tu base de datos

        if (firstEntity != null)
        {
            Console.WriteLine("La conexión con la base de datos se ha establecido correctamente.");
        }
        else
        {
            Console.WriteLine("No se encontraron registros en la tabla.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
    }
}

app.Run();
