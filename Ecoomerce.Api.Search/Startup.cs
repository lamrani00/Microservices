using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Services;
using Ecoomerce.Api.Search.Interfaces;
using Ecoomerce.Api.Search.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Ecoomerce.Api.Search
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

      services.AddHttpClient("OrderService", config =>
      {
        //Récupération le EndPoint de MS Orders
        config.BaseAddress = new Uri(Configuration["Services:Orders"]);
      }
      );

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
