using CordiSimple.Extensions;
using CordiSimple.Seeders;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

//Loading environment variables from the .env file
Env.Load();

// Add services to the container.
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddDatabaseConfiguration(builder.Configuration); 
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => 
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200")
);

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    seeder.SeedDataAsync().Wait();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwaggerServices();
app.Run();
