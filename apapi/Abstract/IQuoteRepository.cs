using apapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apapi.Abstract
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<Quote>> GetAllQuotes();

        Task<Quote> GetQuote(string id);

        // add new Quote document
        Task AddQuote(Quote item);

        // remove a single document / Quote
        Task<bool> RemoveQuote(string id);

        // update just a single document / Quote
        Task<bool> UpdateQuote(Quote Quote);
    }
}
