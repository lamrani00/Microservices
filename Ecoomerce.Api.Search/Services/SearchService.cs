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
    private readonly IProductsService productService;

    public SearchService(IOrdersService orderService, IProductsService productService)
    {
      this.orderService = orderService;
      this.productService = productService;
    }
    // le SearchResults est de type dynamic car on sait pas le resultat de retour.
    public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
    {
      await Task.Delay(1);

      var ordersResult = await orderService.GetOrdersAsync(customerId);
      // Récupération de tous les produits 
      var productsResult = await productService.GetProductsAsync();

      if (ordersResult.IsSuccess)
      {
        // parcourir la liste des orders pour récupérer le nom de produit selon son id 

        foreach (var order in ordersResult.Orders)
        {
          // chercher dans la list les produits demandé dans la commande.
          foreach (var item in order.Items)
          {
            item.ProductName = productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Name;
          }
        }

        return (true, new { ordersResult.Orders });

      }

      return (false, new { ordersResult.ErrorMessage });
    }
  }
}
