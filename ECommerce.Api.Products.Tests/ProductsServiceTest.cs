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
      //  On peut pas �crire la ligne ci-dessous car la classe ProductsDbContext est abstraite
      //         var dbContext = new DbContextOptions()
      // pour �viter �a je vais cr�er une option DbContextOptionsBuilder
      var options = new DbContextOptionsBuilder<ProductsDbContext>()
          // creation des donn�e en m�moire pour utiliser les donn�es factices
          .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
          .Options;
      // mon options est pret pour la rajouter dans mon dbcontext
      var dbContext = new ProductsDbContext(options);
      // Appel � la fonction pour remplir les donn�es factices
      CreateProducts(dbContext);
      // appel de profile pour mapper les donn�e (automapper)
      var productProfile = new ProductProfile();
      var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
      var mapper = new Mapper(configuration);
      // pour ne pas logger j'ai mis null au lieu de Ilogger
      var productsProvider = new ProductsProvider(dbContext, null, mapper);

      // r�cuperation des donn�es � partir de la base de donn�es.

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
    ///  Ajout les donn�es dans la base de donn�es 
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
