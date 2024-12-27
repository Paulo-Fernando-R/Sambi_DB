using db.Index.Operations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

string currentDir = AppDomain.CurrentDomain.BaseDirectory;
string folderName = builder.Configuration.GetSection("Databases").GetValue<string>("FolderName");
string folderPath = Path.Combine(currentDir, folderName);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "SambiDb", Version = "v1" });
});

builder.Services.AddTransient<DatabaseOperations>();
builder.Services.AddTransient<QueryOperations>();
builder.Services.AddTransient<CollectionOperations>();

if (!Directory.Exists(folderPath))
{
    Directory.CreateDirectory(folderPath);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
