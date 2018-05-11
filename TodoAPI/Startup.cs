using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using TodoAPI.Models;

namespace TodoAPI
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
            services.AddResponseCompression();


            services.AddDbContext<TodoContext>(opt =>
                                opt.UseInMemoryDatabase("TodoList"));

            //services.AddDbContext<TodoContext>(options =>
            //       options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                .AddXmlDataContractSerializerFormatters();

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info {
                        Version = "v1",
                        Title = "ToDo API",
                        Description = "A sample ASP.NET Core Web API",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "Abhishek",
                            Email = string.Empty,
                            Url = "http://fhfhfhfhfh.com"
                        },
                        License = new License
                        {
                            Name = "Use under LICX",
                            Url = "https://example.com/license"
                        }

                    });

                    // Configuring swagger to use xml comments of actions methods to describe the API in swagger UI
                    var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseResponseCompression();

            //to allow to send static files like js, css and images
            //app.UseStaticFiles();

            //configue static file with chaching
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
                }
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todos API V1");
                // c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}