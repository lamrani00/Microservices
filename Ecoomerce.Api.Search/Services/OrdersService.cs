using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecoomerce.Api.Search.Services
{
  public class OrdersService : IOrdersService
  {
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<OrdersService> logger;
    public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
    {
      this.httpClientFactory = httpClientFactory;
      this.logger = logger;
    }
    public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
    {
      try
      {
        // Récupération de l'adresse de MS
        var client = httpClientFactory.CreateClient("OrderService");
        // se connecter au service pour récupérer les données
        var response = await client.GetAsync($"api/orders/{customerId}");

        if (response.IsSuccessStatusCode)
        {
          var content = await response.Content.ReadAsByteArrayAsync();
          var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
          var orders = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

          return new(true, orders, response.ReasonPhrase);
        }
        return new(false, null, null);
      }
      catch (Exception ex)
      {
        logger?.LogError("erreur dans l'order Service ", ex);
        return new(false, null, ex.Message);
      }
    }


  }
}