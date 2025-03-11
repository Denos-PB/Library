using System.Text;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Register MongoDB services
builder.Services.AddSingleton<IMongoDatabase>(sp => 
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetSection("MongoDB:ConnectionString").Value 
        ?? throw new InvalidOperationException("MongoDB connection string is not configured");
    var databaseName = configuration.GetSection("MongoDB:DatabaseName").Value 
        ?? throw new InvalidOperationException("MongoDB database name is not configured");

    try
    {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase(databaseName);
        
        // Test connection
        database.RunCommand<MongoDB.Bson.BsonDocument>(new MongoDB.Bson.BsonDocument("ping", 1));
        Console.WriteLine("Successfully connected to MongoDB!");
        
        return database;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to connect to MongoDB: {ex.Message}");
        throw;
    }
});


builder.Services.AddIdentity<Reader, IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

})
    .AddDefaultTokenProviders();
    

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultScheme = 
    options.DefaultSignInScheme = 
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
    
// Register repositories
builder.Services.AddScoped<BooksInterfaces, BooksRepository>();
builder.Services.AddScoped<AuthorInterfaces, AuthorRepository>();

// Register services
builder.Services.AddScoped<BooksService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<ReaderService>();

// Configure API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure middleware
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

// Map endpoints
app.MapControllers();

app.Run();