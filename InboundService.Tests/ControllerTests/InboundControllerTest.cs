/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InboundService.Tests
{
    using System;
    using System.Threading.Tasks;
    using DepotDatabase;
    using InboundService.Controllers;
    using InboundService.Models;
    using InboundService.Repository;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Contains all the test methods of Inbound service
    /// </summary>
    [TestClass]
    public class InboundControllerTest
    {
        [TestMethod]
        public void CreateInboundOrderWillCreateOrder()
        {
            // Arrange
            var mockRepository = new Mock<IInboundRepository>();
            InboundData data = new InboundData()
            {
                ProductName = "Product 1",
                Supplier = "Supplier 1",
                ProductTypeId = 1,
                CreatedDate = DateTime.Now,
            };
            var order = new InboundOrder()
            {
                Id = 5,
                CreatedDate = DateTime.Now,
                ProductName = "Product 1",
                Supplier = "Supplier 1",
                ProductTypeId = 1,
            };
            mockRepository.Setup(x => x.CreateInboundOrder(data))
                .Returns(Task.FromResult(order));

            //Act
            var controller = new InboundController(mockRepository.Object);

            // Act
            var actionResult = controller.CreateInboundOrder(data);
            var contentResult = (actionResult.Result as CreatedAtActionResult).Value as InboundOrder;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Id);
        }

        [TestMethod]
        public void AssignPalletItemWillCreateProduct()
        {
            // Arrange
            var mockRepository = new Mock<IInboundRepository>();
            
            var product = new Product()
            {
                Id = 5,
                Name = "Product 1",
                AddedDate = DateTime.Now,
                PalletId = 1
            };
            mockRepository.Setup(x => x.AssignPalletItem(1))
                .Returns(Task.FromResult(product));

            //Act
            var controller = new InboundController(mockRepository.Object);

            // Act
            var actionResult = controller.AssignPalletItem(1);
            var contentResult = (actionResult.Result as CreatedAtActionResult).Value as Product;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Id);
        }

        [TestMethod]
        public void ReassignPalletItemWillUpdateProduct()
        {
            // Arrange
            var mockRepository = new Mock<IInboundRepository>();

            var reassignData = new ReassignData()
            {
                ProductId = 1,
                PalletId = 1
            };
            var product = new Product()
            {
                Id = 5,
                Name = "Product 1",
                AddedDate = DateTime.Now,
                PalletId = 1
            };
            mockRepository.Setup(x => x.ReassignPalletItem(reassignData))
                .Returns(Task.FromResult(product));

            //Act
            var controller = new InboundController(mockRepository.Object);

            // Act
            var actionResult = controller.ReassignPalletItem(reassignData);
            var contentResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Pallet item/product Updated", contentResult.Value);
        }

        [TestMethod]
        public void GetItemQuantityReturnsQuantity()
        {
            // Arrange
            var mockRepository = new Mock<IInboundRepository>();
            int productId = 1;
            
            mockRepository.Setup(x => x.GetItemQuantity(productId))
                .Returns(5);

            //Act
            var controller = new InboundController(mockRepository.Object);

            // Act
            var actionResult = controller.GetItemQuantity(productId);
            var contentResult = actionResult as ObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Value);
        }
    }
}
