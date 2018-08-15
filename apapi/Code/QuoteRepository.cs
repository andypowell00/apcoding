using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apapi.Abstract;
using apapi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace apapi.Code
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly DbContext _context = null;

        public QuoteRepository(IOptions<Settings> settings)
        {
            _context = new DbContext(settings);
        }

        public async Task<IEnumerable<Quote>> GetAllQuotes()
        {
            try
            {
                return await _context.Quotes
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId(BSonId value)


        public async Task<Quote> GetQuote(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Quotes
                                .Find(Quote =>  Quote.InternalId == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddQuote(Quote item)
        {
            try
            {
                await _context.Quotes.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveQuote(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Quotes.DeleteOneAsync(
                        Builders<Quote>.Filter.Eq("_id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateQuote(Quote Quote)
        { 
            
            ReplaceOneResult updateResult =
            await _context
            .Quotes
            .ReplaceOneAsync(
            filter: g => g.InternalId == Quote.InternalId,
            replacement: Quote);
            return updateResult.IsAcknowledged
            && updateResult.ModifiedCount > 0;
            
        }

        public async Task<bool> UpdateQuote(string id, Quote item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Quotes
                                    .ReplaceOneAsync(n => n.InternalId.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllQuotes()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Quotes.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
