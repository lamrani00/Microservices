using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
  [ApiController]
  [Route("api/products")]

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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProdductByIdAsync(int id)
    {
      var result = await productsProvider.GetProductByIdAsync(id);
      if (result.IsSuccess)
      {
        return Ok(result.Product);
      }
      else
        return NotFound();
    }



  }
}