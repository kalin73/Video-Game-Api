using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoGameApi.Config;
using VideoGameApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<VideoGameService>();

MongoDBSettings? mongoDbSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

String? username = Environment.GetEnvironmentVariable("username");
String? password = Environment.GetEnvironmentVariable("password");
String url = $"mongodb+srv://{username}:{password}@mongodb.ldc9z5l.mongodb.net/?retryWrites=true&w=majority";

builder.Services.AddDbContext<VideoGameDbContext>(options => options
    .UseMongoDB(url, mongoDbSettings.DatabaseName ?? ""));

builder.Services.AddControllers();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();