
using CarPrime.Data;
using CarPrime.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors config for allowing front end to make requests to this api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsBuilder =>
            corsBuilder.AllowAnyOrigin()  // Allows all origins
                .AllowAnyHeader()  // Allows all headers
                .AllowAnyMethod()  // Allows all HTTP methods
    );
});

//Database config
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//CustomerService
builder.Services.AddScoped<ICustomerService, CustomerService>();

//RentalService
builder.Services.AddScoped<IRentalService, RentalService>();

//CompaniesService
builder.Services.AddScoped<CompaniesService, CompaniesService>();
builder.Services.AddScoped<CarPrimeService, CarPrimeService>();
builder.Services.AddHealthChecks();
//Google authentication config
var app = builder.Build();

// CORS should be applied before MapControllers and UseAuthentication and Swagger ig
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync(); 
    await db.SeedDb();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();