using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using MInimalApiPelis;
using MInimalApiPelis.Entidades;


var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!;
//Aqui van los servicios

//Conectar con la base de datos
builder.Services.AddDbContext<ApplicactionDbContext>(opciones =>
    opciones.UseSqlServer("name=DefaultConnection"));

//Configuracion de CORS
builder.Services.AddCors(opciones =>
{
    
    //Agregando politica
    opciones.AddDefaultPolicy(config =>
    {
        config.WithOrigins(origenesPermitidos).AllowAnyHeader().AllowAnyMethod();
    });
    
    
    //Agregando politica allowAnyOrigin
    opciones.AddPolicy("libre", configuration =>
    {
        configuration.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
    
});


builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Fin servicios

var app = builder.Build();

//Inicio de la area de middleware

/*if (builder.Environment.IsDevelopment())
{

}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseOutputCache();


app.MapGet("/",   [EnableCors(policyName:"libre")]() => "Hola Santo");

app.MapGet("/generos", () =>
{
    var generos = new List<Genero>()
    {

        new Genero()
        {
            Id = 1,
            Nombre = "Terror"
        },
        new Genero()
        {
            Id = 2,
            Nombre = "Accion"
        },
        new Genero()
        {
            Id = 3,
            Nombre = "Comedia"
        }
    };

    return generos;
}).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(15)));

app.MapGet("/nopor", () =>
{
    var generos = new List<Genero>()
    {
        new Genero()
        {
            Id = 1,
            Nombre = "Fernando en 4"
        },
        new Genero()
        {
            Id = 2,
            Nombre = "Santiago la boquichula"
        },
        new Genero()
        {
            Id = 3,
            Nombre = "Mateo la jugetona"
        }
    };

    return generos;
});


//Fin de los middleware
app.Run();