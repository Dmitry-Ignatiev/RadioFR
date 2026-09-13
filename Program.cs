using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RadioDbContext>(options =>
{
    options.UseNpgsql(
    builder.Configuration.GetConnectionString("RadioDatabase")
);
    
});
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();


// radios
var radioDevice1 = new RadioDevice
{
    Id = 1,
    Name = "Radio 1",
    Frequency = 101.5f,
    IsOnline = true
};

var radioDevice2 = new RadioDevice
{
    Id = 2,
    Name = "Radio 2",
    Frequency = 102.5f,
    IsOnline = false
};

var radios = new List<RadioDevice>
{
    radioDevice1,
    radioDevice2
};

app.MapGet("/radios", () => radios);
app.MapGet("/", () => "Welcome to radio device API!");
app.MapGet("/radios/{id}", (int id) =>
{
var radio = radios.FirstOrDefault(r => r.Id == id);
if (radio == null)
{
    return Results.NotFound();
}
else
{
    return Results.Ok(radio);
}
});

app.MapPost("/radios",(RadioDevice radio)=>
{
    radio.Id = radios.Count + 1;
    radios.Add(radio);
    return Results.Created($"/radios/{radio.Id}",  radio);
});
app.MapDelete("/radios/{id}", (int id) =>
{
    var radio = radios.FirstOrDefault(r => r.Id == id);
    if (radio == null)
    {
        return Results.NotFound();
    }
    else
    {
        radios.Remove(radio);
        return Results.NoContent();
    }
});
app.MapPut("/radios/{id}", (int id, RadioDevice updatedRadio) =>
{
    var radio = radios.FirstOrDefault(r => r.Id == id);
    if (radio == null)
    {
        return Results.NotFound();
    }
    else
    {
        radio.Name = updatedRadio.Name;
        radio.Frequency = updatedRadio.Frequency;
        radio.IsOnline = updatedRadio.IsOnline;
        return Results.Ok(radio);
    }
});
app.Run();

