/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InventoryManagement.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DepotDatabase;
    using InventoryManagement.Controllers;
    using InventoryManagement.Models;
    using InventoryManagement.Repository;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Class includes test methods of inventory management service
    /// </summary>
    [TestClass]
    public class InventoryControllerTest
    {      
        [TestMethod]
        public void GetPalletDetailsReturnsWithData()
        {
            // Arrange
            var mockRepository = new Mock<IInventoryRepository>();

            List<PalletSearchResult> searchResultList = new ();
            PalletSearchResult palletSearchResult = new ()
            {
               NodeLocation = "TestLocation",
               MaxQuantity = 10,
               Priority = "Medium",
               ProductType = "Type1",
               Quantity = 2
            };
            searchResultList.Add(palletSearchResult);
            var searchFilter = new SearchFilter() { PageNumber = 1, RowsOfPage = 10 };
            mockRepository.Setup(x => x.GetPalletData(searchFilter))
                .Returns(Task.FromResult(searchResultList));

            var controller = new InventoryController(mockRepository.Object);

            // Act
            var actionResult = controller.GetPalletDetails(searchFilter);
            var contentResult = actionResult.Result.Value;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("TestLocation", contentResult.FirstOrDefault().NodeLocation);
        }

        [TestMethod]
        public void GetAllOrdersReturnsWithData()
        {
            // Arrange
            var mockRepository = new Mock<IInventoryRepository>();

            List<OrderSearchResult> searchResultList = new();
            OrderSearchResult orderSearchResult = new()
            {
               OrderStatus = "Accepted",
               ShipmentDate = DateTime.Now.AddDays(-1),
               ShipmentStatus = "InProgress",
               CreatedDate = DateTime.Now.AddDays(-2),
               ProductName = "Product1"
            };
            searchResultList.Add(orderSearchResult);
            var searchFilter = new SearchFilter() { PageNumber = 1, RowsOfPage = 10 };
            mockRepository.Setup(x => x.GetAllOrders(searchFilter))
                .Returns(Task.FromResult(searchResultList));

            var controller = new InventoryController(mockRepository.Object);

            // Act
            var actionResult = controller.GetAllOrders(searchFilter);
            var contentResult = actionResult.Result.Value;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Accepted", contentResult.FirstOrDefault().OrderStatus);
            Assert.AreEqual("InProgress", contentResult.FirstOrDefault().ShipmentStatus);
        }

        [TestMethod]
        public void SearchProductsReturnsWithData()
        {
            // Arrange
            string searchProductInput = "prod";
            var mockRepository = new Mock<IInventoryRepository>();
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Name = "Product1",
                    AddedDate = DateTime.Now
                },
                new Product()
                {
                    Name = "Product2",
                    AddedDate = DateTime.Now
                }
            };
            
            mockRepository.Setup(x => x.SearchProduct(searchProductInput))
                .Returns(Task.FromResult(products));

            var controller = new InventoryController(mockRepository.Object);

            // Act
            var actionResult = controller.SearchProducts(searchProductInput);
            var contentResult = actionResult.Result.Value;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(2, contentResult.Count());
        }


        [TestMethod]
        public void GetProductDetailsReturnsWithData()
        {
            // Arrange
            
            var mockRepository = new Mock<IInventoryRepository>();
            ProductSearchResult searchResult = new ProductSearchResult()
            {
                AddedDate = DateTime.Now,
                Name = "Product1"
            };
            mockRepository.Setup(x => x.GetProductDetails(1))
                .Returns(Task.FromResult(searchResult));

            var controller = new InventoryController(mockRepository.Object);

            // Act
            var actionResult = controller.GetProductDetails(1);
            var contentResult = actionResult.Result.Value;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Product1", contentResult.Name);
        }

        [TestMethod]
        public void GetProductLocationReturnsWithData()
        {
            // Arrange

            var mockRepository = new Mock<IInventoryRepository>();
            
            mockRepository.Setup(x => x.GetProductLocation(1))
                .Returns("South");

            var controller = new InventoryController(mockRepository.Object);

            // Act
            var actionResult = controller.GetProductLocation(1);
            var contentResult = actionResult as ObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("South", contentResult);
        }

        [TestMethod]
        public void GetOrderStatusReturnsWithData()
        {
            // Arrange

            var mockRepository = new Mock<IInventoryRepository>();

            mockRepository.Setup(x => x.GetOrderStatus(1))
                .Returns(Task.FromResult("Accepted"));

            var controller = new InventoryController(mockRepository.Object);

            // Act
            var actionResult = controller.GetOrderStatus(1);
            var contentResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Accepted", contentResult.Value);
        }

        [TestMethod]
        public void UpdateProductInfoUpdatesProductData()
        {
            // Arrange

            var mockRepository = new Mock<IInventoryRepository>();
            Product product = new Product()
            {
                Name = "Product1",
                AddedDate = DateTime.Now,
                PalletId = 1
            };
            bool? result = true;
            mockRepository.Setup(x => x.UpdateProductInformation(product))
                .Returns(Task.FromResult(result));

            var controller = new InventoryController(mockRepository.Object);

            // Act
            var actionResult = controller.UpdateProductInformation(product);
            var contentResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Product Updated", contentResult.Value);
        }

    }
}
