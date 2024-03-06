using CerveceriaAPI;
using CerveceriaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<ICervezaService, CervezaService>();


var app = builder.Build();

// Enable CORS
app.UseCors(options =>
{
    //Metodo para abilitar la api a ser consumida por una url en espcifico
    //options.WithOrigins("http://your-application-domain.com");
    //Metodo para abilitar la api a ser cosumida desde cualquier url
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
