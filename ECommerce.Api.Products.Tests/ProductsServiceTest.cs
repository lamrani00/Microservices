using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Products.Tests
{
  public class ProductsServiceTest
  {
    [Fact]
    public async Task GetProductsReturnsAllProducts()
    {
      //  On peut pas écrire la ligne ci-dessous car la classe ProductsDbContext est abstraite
      //         var dbContext = new DbContextOptions()
      // pour éviter ça je vais créer une option DbContextOptionsBuilder
      var options = new DbContextOptionsBuilder<ProductsDbContext>()
          // creation des donnée en mémoire pour utiliser les données factices
          .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
          .Options;
      // mon options est pret pour la rajouter dans mon dbcontext
      var dbContext = new ProductsDbContext(options);
      // Appel à la fonction pour remplir les données factices
      CreateProducts(dbContext);
      // appel de profile pour mapper les donnée (automapper)
      var productProfile = new ProductProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
      var mapper = new Mapper(configuration);
      // pour ne pas logger j'ai mis null au lieu de Ilogger
      var productsProvider = new ProductsProvider(dbContext, null, mapper);

      // récuperation des données à partir de la base de données.

      var product = await productsProvider.GetProductAsync();
      Assert.True(product.IsSuccess);
      Assert.True(product.Products.Any());
      Assert.Null(product.ErrorMessage);
    }



    [Fact]
    // Valide Id
    public async Task GetProductReturnsProductUsingValidId()
    {
      var options = new DbContextOptionsBuilder<ProductsDbContext>()
          .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
          .Options;
      var dbContext = new ProductsDbContext(options);
      CreateProducts(dbContext);

      var productProfile = new ProductProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
      var mapper = new Mapper(configuration);

      var productsProvider = new ProductsProvider(dbContext, null, mapper);

      var product = await productsProvider.GetProductByIdAsync(1);
      Assert.True(product.IsSuccess);
      Assert.NotNull(product.Product);
      Assert.True(product.Product.Id == 1);
      Assert.Null(product.ErrorMessage);
    }

    [Fact]
    // invalide Id
    public async Task GetProductReturnsProductUsingInvalidId()
    {
      var options = new DbContextOptionsBuilder<ProductsDbContext>()
          .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInvalidId))
          .Options;
      var dbContext = new ProductsDbContext(options);
      CreateProducts(dbContext);

      var productProfile = new ProductProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
      var mapper = new Mapper(configuration);

      var productsProvider = new ProductsProvider(dbContext, null, mapper);

      var product = await productsProvider.GetProductByIdAsync(-1);
      Assert.False(product.IsSuccess);
      Assert.Null(product.Product);
      Assert.NotNull(product.ErrorMessage);
    }

    /// <summary>
    ///  Ajout les données dans la base de données 
    /// </summary>
    /// <param name="dbContext"></param>
    private void CreateProducts(ProductsDbContext dbContext)
    {
      for (int i = 1; i <= 10; i++)
      {
        dbContext.Products.Add(new Product()
        {
          Id = i,
          Name = Guid.NewGuid().ToString(),
          Inventory = i + 10,
          Price = (decimal)(i * 3.14)
        });
      }
      dbContext.SaveChanges();
    }
  }
}
