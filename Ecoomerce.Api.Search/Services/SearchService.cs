using Ecommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
  public class SearchService : ISearchService
  {
    private readonly IOrdersService orderService;

    public SearchService(IOrdersService orderService)
    {
      this.orderService = orderService;
    }
    // le SearchResults est de type dynamic car on sait pas le resultat de retour.
    public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
    {
      await Task.Delay(1);

      var orderResult = await orderService.GetOrdersAsync(customerId);

      if (orderResult.IsSuccess)
      {
        return (true, new { orderResult.Orders });
      }

      return (false, new { orderResult.ErrorMessage });
    }
  }
}
