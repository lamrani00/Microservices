using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Services;
using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Ecommerce.Api.Search
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
      services.AddScoped<ISearchService, SearchService>();
      services.AddScoped<IOrdersService, OrdersService>();
      services.AddScoped<ICustomersService, CustomersService>();
      services.AddScoped<IProductsService, ProductsService>();
      //Récupération le EndPoint de MS Orders
      services.AddHttpClient("OrderService", config =>
      {
        config.BaseAddress = new Uri(Configuration["Services:Orders"]);
      });
      //Récupération le EndPoint de MS Products
      services.AddHttpClient("ProductService", config =>
      {
        config.BaseAddress = new Uri(Configuration["Services:Products"]);
      });



      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
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
