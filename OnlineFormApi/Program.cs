using OnlineForm.Services;
using OnlineForm.Services.Abstractions;
using OnlineFormApi.Data;
using OnlineFormApi.Services;
using OnlineFormApi.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterOnlineFormServices(builder.Configuration);

string blazorOnlineForm = "_blazorOnlineForm";
builder.Services.AddCors(o => o.AddPolicy(name: blazorOnlineForm,
          builder =>
          {
              builder.WithOrigins("https://localhost:7222")
              .AllowAnyMethod()
              .AllowAnyHeader();
          }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(blazorOnlineForm);
app.UseAuthorization();
app.MapControllers();

app.Run();
