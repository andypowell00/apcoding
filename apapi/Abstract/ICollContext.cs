using apapi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apapi.Abstract
{
    public class ICollContext
    {
        IMongoCollection<Todo> Todos { get; }
        IMongoCollection<Quote> Quotes { get; }
    }
}
