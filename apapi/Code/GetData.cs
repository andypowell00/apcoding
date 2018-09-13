using System;
using System.Collections.Generic;
using System.Net.Http;
using apapi.Models;
using HtmlAgilityPack;

namespace apapi.Code
{
    public class GetData {

        public async System.Threading.Tasks.Task<List<Quote>> GetQuoteAsync(string url, string expath, string source, int numLoops, string auth){
            List<Quote> q = new List<Quote>();
            int x = 1;
            
            while(x<=numLoops){
            HttpClient client = new HttpClient();
         
            var urlstring = url;
            var quotexpath = expath;
            var src = source;
            var au = auth;
                
            var response = await client.GetAsync(urlstring);
            
            var pageContents = await response.Content.ReadAsStringAsync();
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);
            
            var nodes = pageDocument.DocumentNode.SelectNodes(quotexpath);
            
            
            foreach(var node in nodes){
                var quoteString = node.InnerText;
                
                    q.Add(new Quote{
                        source = src,
                        title = src,
                        description = quoteString,
                        author = au
                    });
                }//end foreach
            
            x++;

            }//while
         
            return q;


        }

    }

}