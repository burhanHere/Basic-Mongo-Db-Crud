
using MongooCrud.Dtos;
using MongooCrud.Services;

var builder = WebApplication.CreateBuilder(args);

// seting up configurations
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbConnectionSettings"));

// Add services to the container.
builder.Services.AddScoped<DbConnectionService>();
builder.Services.AddScoped<BookService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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
