using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
  [Route("api/products")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly IProductsProvider productsProvider;

    public ProductsController(IProductsProvider productsProvider)
    {
      this.productsProvider = productsProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetProdductsAsync()
    {
      var result = await productsProvider.GetProductAsync();
      if (result.IsSuccess)
      {
        return Ok(result.Products);
      }
      else
        return NotFound();
    }
  }
}