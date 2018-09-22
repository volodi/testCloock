using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cloocks.Server.Extensions
{
    public static class AppBuildExtensions
    {
        public static void UseMongo(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var dbConnectionString = configuration["MongoConnection:ConnectionString"];
            var dbDatabase = configuration["MongoConnection:Database"];
            MongoFactory.SetSetting(new MongoConnectionString(dbConnectionString, dbDatabase));
        }
    }

    public class MongoFactory
    {
        private static IMongoDatabase mongoDatabase;

        public static void SetSetting(MongoConnectionString setting)
        {
            mongoDatabase = new MongoClient(setting.ConnectionString).GetDatabase(setting.GeneralDatabase.ToLower());
        }

        public static IMongoCollection<T> GetCollection<T>()
        {
            return mongoDatabase.GetCollection<T>(typeof(T).Name + "s");
        }
    }

    public class MongoConnectionString
    {
        public MongoConnectionString(string connectionString, string database)
        {
            ConnectionString = connectionString;
            GeneralDatabase = database;
        }

        public string ConnectionString { get; set; }
        public string GeneralDatabase { get; set; }
    }
}
