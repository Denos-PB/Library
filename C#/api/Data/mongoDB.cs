using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Identity;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace api.Data
{
    public class MongoDB : IdentityDbContext<Reader>
    {
        private readonly IMongoDatabase _database;

        public MongoDB(IConfiguration configuration) : base(new DbContextOptions<IdentityDbContext<Reader>>())
        {
            var connectionString = configuration.GetSection("MongoDB:ConnectionString").Value;
            var databaseName = configuration.GetSection("MongoDB:DatabaseName").Value;

            try
            {
                var settings = MongoClientSettings.FromConnectionString(connectionString);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                var client = new MongoClient(settings);
                _database = client.GetDatabase(databaseName);

                var result = _database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB Connection Error: {ex.Message}");
                throw;
            }
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}