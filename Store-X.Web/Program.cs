
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_X.Domain.Contracts;
using Store_X.Persistence;
using Store_X.Persistence.Data.Contexts;
using Store_X.Services;
using Store_X.Services.Mapping.Products;
using Store_X.Services_Abstractions;
using Store_X.Shared.ErrorModels;
using Store_X.Web.Extentions;
using Store_X.Web.Middlewares;
using System.Threading.Tasks;

namespace Store_X.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAllServices(builder.Configuration);



            var app = builder.Build();

            await app.ConfigureMiddlewaresAsync();

            app.Run();
        }
    }
}
