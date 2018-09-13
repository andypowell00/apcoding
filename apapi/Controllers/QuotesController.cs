using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apapi.Abstract;
using apapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using HtmlAgilityPack;

namespace apapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class QuoteController : Controller
    {
        private readonly IQuoteRepository _QuoteRepository;

        public QuoteController(IQuoteRepository QuoteRepository)
        {
            _QuoteRepository = QuoteRepository;
        }

        //[NoCache]
        [HttpGet]
        public async Task<IEnumerable<Quote>> Get()
        {
            //make this re usable, take out hard coding and give params,  url, 2 xpaths for quote, source, and auth (or episode if tv)
            return await _QuoteRepository.GetAllQuotes();
            //string url, string quotexpath,string src
            //
           
           
        }

        // GET api/Quote/5 - retrieves a specific Quote using InternalId (BSonId)
        [HttpGet("{id}")]
        public async Task<Quote> Get(string id)
        {
            return await _QuoteRepository.GetQuote(id) ?? new Quote();

        }

        // POST api/Quote - creates a new Quote
        [HttpPost]
        public void Post([FromBody]Quote newQuote)
        {
            _QuoteRepository.AddQuote(new Quote
            {
                title = newQuote.title,
                description = newQuote.description
            });
        }

        // PUT api/Quote/5 - updates a specific Quote
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            // _QuoteRepository.UpdateQuoteDocument(id, value);
        }

        // DELETE api/Quote/5 - deletes a specific Quote
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _QuoteRepository.RemoveQuote(id);
        }
    }
}
