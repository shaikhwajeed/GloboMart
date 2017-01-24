using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Product.Catalogue.Controllers;
using GloboMart.Common.Models;
using System.Collections.Generic;
using GloboMart.Common.Helpers;

namespace Product.Catalogue.Tests.Controllers
{
    [TestClass]
    public class ProductCatalogueControllerTest
    {

        [TestInitialize()]
        public void Initialize()
        {
            DocumentDBRepositoryHelper<ProductDetails>.Initialize();
        }
        
        [TestMethod]
        public void Get()
        {
            // Arrange
            ProductCatalogueController controller = new ProductCatalogueController();

            // Act
            var result = controller.Get().Result;
            
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            ProductCatalogueController controller = new ProductCatalogueController();

            var product = new ProductDetails();
            product.Name = "TestProduct";
            product.Type = ProductType.Arts;
            
            // Test Create API
            var result = controller.CreateAsync(product).Result;
            Assert.IsNotNull(result);

            // Test Get Product Info API
            var productFromDb = controller.GetProductInfo(result).Result;
            Assert.IsNotNull(productFromDb);
            Assert.IsTrue(productFromDb.Name == "TestProduct");
            Assert.IsTrue(productFromDb.Type== ProductType.Arts);
            Assert.IsNotNull(productFromDb.Id == result);

            // Test Delete API
            var isDeleted = controller.DeleteProduct(result).Result;
            Assert.IsTrue(isDeleted);

        }

    }
}
