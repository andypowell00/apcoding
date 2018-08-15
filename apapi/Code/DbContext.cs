using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using apapi.Models;
using apapi.Abstract;

namespace apapi.Code
{
    public class DbContext : ICollContext
    {
        private readonly IMongoDatabase _database = null;

        public DbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<Todo> Todos
        {
            get
            {
                return _database.GetCollection<Todo>("Todo");
            }
        }
        public IMongoCollection<Quote> Quotes
        {
            get
            {
                return _database.GetCollection<Quote>("Quotes");
            }
        }
    }
}
