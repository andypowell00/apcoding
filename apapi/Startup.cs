using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apapi.Abstract;
using apapi.Code;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace apapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
        });
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:58619").AllowAnyHeader()
                .AllowAnyMethod());
        });

            services.Configure<Settings>(
            options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
                options.TMApiKey = Configuration.GetSection("TMAPI:ApiKey").Value;
                options.TMBaseUrl= Configuration.GetSection("TMAPI:BaseUrl").Value;
                options.OWMApiKey = Configuration.GetSection("OWMAPI:ApiKey").Value;
                options.OWMBaseUrl= Configuration.GetSection("OWMAPI:BaseUrl").Value;
            });
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<IQuoteRepository, QuoteRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            app.UseCors("AllowSpecificOrigin");

            app.UseMvc();
        }
    }
}
