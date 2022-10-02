using Server_Test_Project.FIleStorage.Implimentations;
using Server_Test_Project.FIleStorage.Interfaces;
using Server_Test_Project.JsonConventer.Implimentations;
using Server_Test_Project.JsonConventer.Interfaces;
using Server_Test_Project.Repository.Implimentations;
using Server_Test_Project.Repository.Interfaces;
using Server_Test_Project.Services.Implimentations;
using Server_Test_Project.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddScoped<IImageStorage>(provider => new ImageStorage("Data"));
builder.Services.AddTransient<IJsonConverter, JsonConverter>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
