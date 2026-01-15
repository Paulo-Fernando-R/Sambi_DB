using db.Index.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Windows Service support
builder.Host.UseWindowsService();

string currentDir = AppDomain.CurrentDomain.BaseDirectory;
string folderName = builder.Configuration.GetSection("Databases").GetValue<string>("FolderName");
string folderPath = Path.Combine(currentDir, folderName);

string adminDir = builder.Configuration.GetSection("Admin").GetValue<string>("Directory");


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "SambiDb", Version = "v1" });
});

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddTransient<DatabaseOperations>();
builder.Services.AddTransient<QueryOperations>();
builder.Services.AddTransient<CollectionOperations>();
builder.Services.AddTransient<RegisterOperations>();

if (!Directory.Exists(folderPath))
{
    Directory.CreateDirectory(folderPath);
}

if (!Directory.Exists(adminDir))
{
    Directory.CreateDirectory(adminDir);
}

var app = builder.Build();


app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapFallbackToFile("index.html");

app.MapControllers();

//Standalone build = dotnet publish -c Release -r win-x64 --self-contained

app.Run("http://localhost:5000");
//app.Run();
