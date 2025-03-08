using NayaViewAnimeApi.Application;
using DotNetEnv;
using NayaViewAnimeApi.Infrastructure;
Env.Load();

var builder = WebApplication.CreateBuilder(args);

//main services
builder.Services.AddHttpClient<IExternalApiService, ExternalApiService>();


//other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();