using Application.Common.Mappings;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Persistence.DbContexts;
using Infrastructure.Persistence.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace MovieDatabase.API
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


            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters()
              .ConfigureApiBehaviorOptions(setupAction =>
              {
                  setupAction.InvalidModelStateResponseFactory = context =>
                  {
                      // create a problem details object
                      var problemDetailsFactory = context.HttpContext.RequestServices
                      .GetRequiredService<ProblemDetailsFactory>();
                      var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                          context.HttpContext,
                          context.ModelState);

                      // add additional info not added by default
                      problemDetails.Detail = "See the errors field for details.";
                      problemDetails.Instance = context.HttpContext.Request.Path;

                      // find out which status code to use
                      var actionExecutingContect =
                            context as ActionExecutingContext;

                      // if there are modelstate errors & all arguments were correctly
                      // found/parsed we're dealing with validation errors
                      if ((context.ModelState.ErrorCount > 0) &&
                          (actionExecutingContect?.ActionArguments.Count ==
                          context.ActionDescriptor.Parameters.Count))
                      {
                          problemDetails.Type = "https://moviedatabase.com/modelvalidationproblem";
                          problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                          problemDetails.Title = "One or more validation errors occurred.";

                          return new UnprocessableEntityObjectResult(problemDetails)
                          {
                              ContentTypes = { "application/problem+json" }
                          };
                      };

                      problemDetails.Status = StatusCodes.Status400BadRequest;
                      problemDetails.Title = "One or more errors on input occurred.";
                      return new BadRequestObjectResult(problemDetails)
                      { 
                          ContentTypes = { "application/problem+json" }
                      };
                  };
              });


            services.AddAutoMapper(typeof(MapperProfiles));

            services.AddMediatR(AppDomain.CurrentDomain.Load("Application"));
            services.AddScoped<IMovieDatabaseRepository, MovieDatabaseRepository>();



            services.AddDbContext<MovieDatabaseContext>(options =>
            {
                options.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=MovieDatabaseDB;Trusted_Connection=True;");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });
                });
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
