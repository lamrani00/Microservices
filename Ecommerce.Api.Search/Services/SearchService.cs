using Ecommerce.Api.Search.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
  public class SearchService : ISearchService
  {
    private readonly IOrdersService orderService;
    private readonly IProductsService productService;
    private readonly ICustomersService customersService;

    public SearchService(IOrdersService orderService, IProductsService productService, ICustomersService customersService)
    {
      this.orderService = orderService;
      this.productService = productService;
      this.customersService = customersService;
    }
    // le SearchResults est de type dynamic car on sait pas le resultat de retour.
    public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
    {
      await Task.Delay(1);
      var customersResult = await customersService.GetCustomerAsync(customerId);
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
            // Si on n'arrive pas à récupérer le nom de produit on peut donner un nom par défaut comme  "le nom est n'est pas disponible"
            item.ProductName = productsResult.IsSuccess ?
                                                          productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Name
                                                          :
                                                          "le nom est n'est pas disponible";
          }
        }

        var result = new
        {
          Customer = customersResult.IsSuccess ?
                        customersResult.Customer :
                        new { Name = "Customer information is not available" },
          Orders = ordersResult.Orders
        };

        return (true, result);


      }

      return (false, new { ordersResult.ErrorMessage });
    }
  }
}
