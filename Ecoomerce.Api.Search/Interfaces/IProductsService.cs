using Ecommerce.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecoomerce.Api.Search.Interfaces
{
  public interface IProductsService
  {
    Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
  }
}
