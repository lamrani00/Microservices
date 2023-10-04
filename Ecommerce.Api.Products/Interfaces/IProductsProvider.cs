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
    ///   Renvoie un tuple
    /// </summary>
    /// <returns></returns>
    Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessag)> GetProductAsync();
    /// <summary>
    /// Renvoi un produit  
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductByIdAsync(int id);
  }
}