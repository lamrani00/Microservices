using Ecommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Interfaces
{
  public interface IProductsProvider
  {
    /// <summary>
    ///   Cette Classe renvoie un tuple
    /// </summary>
    /// <returns></returns>
    Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessag)> GetProductAsync();
  } 
}
