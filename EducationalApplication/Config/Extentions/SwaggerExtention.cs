using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;

namespace EducationalApplication.Config.Extentions
{
    public static class SwaggerExtention
    {
        public static IServiceCollection AddOurSwaager(this IServiceCollection services)
        {
            // Swagger service properties
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Auth JWT",
                    Description = "Auth JWT Demo - angular-netcore.ir",
                    
                //    TermsOfService = "None",
                Contact = new OpenApiContact()
                    {
                        Name = "Mohammad Moein Fazeli",
                        Email = "mmfazeli372@gmail.com",
                        //    Url = "http://www.angular-netcore.ir"
                       
            }
                });
           
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //    {
                //        { "Bearer", List<string>{}}
                //    }

                //);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
              
            });
            return services;
        }
    }




}
