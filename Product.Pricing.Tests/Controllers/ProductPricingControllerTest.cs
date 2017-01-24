using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GloboMart.Common.Models;
using Product.Pricing.Controllers;

namespace Product.Pricing.Tests.Controllers
{
    [TestClass]
    public class ProductPricingControllerTest
    {
        [TestInitialize()]
        public void Initialize()
        {
            GloboMart.Common.Helpers.DocumentDBRepositoryHelper<ProductPrice>.Initialize();
        }


        [TestMethod]
        public void Get()
        {
            // Arrange
            ProductPricingController controller = new ProductPricingController();

            // Act
            var result = controller.Get().Result;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            ProductPricingController controller = new ProductPricingController();
            const string productId = "5171b66f-7d14-4d6a-8de9-44927f6cfb2d";
            var product = new ProductPrice();
            product.Id = productId;
            product.Price = 200;

            // Test Create API
            var result = controller.CreateAsync(product).Result;
            Assert.IsNotNull(result);

            // Test Get Price Info API
            var productFromDb = controller.GetPriceInfo(result).Result;
            Assert.IsNotNull(productFromDb);
            Assert.IsTrue(productFromDb.Id == productId);
            Assert.IsTrue(productFromDb.Price == 200);
            Assert.IsNotNull(productFromDb.Id == result);

            // Test Delete API
            var isDeleted = controller.DeletePriceData(result).Result;
            Assert.IsTrue(isDeleted);

        }
    }
}
