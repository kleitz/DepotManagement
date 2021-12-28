/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Tests
{
    using System;
    using System.Threading.Tasks;
    using DepotDatabase;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using OutboundService.Controllers;
    using OutboundService.Models;
    using OutboundService.Repository;

    /// <summary>
    /// Class includes test methods of outbound service
    /// </summary>
    [TestClass]
    public class OutboundControllerTests
    {
        [TestMethod]
        public void CreateCustomerOrderWillCreateData()
        {
            // Arrange
            var mockRepository = new Mock<IOutboundRepository>();
            CustomerOrderData data = new CustomerOrderData()
            {
                Quantity = 1,
                OrderDate = DateTime.Now,
                ProductId = 1,
                UserId = 1
            };
            
            mockRepository.Setup(x => x.CreateCustomerOrder(data))
                .Returns(Task.FromResult(new CustomerOrder() { Id=5, CreatedDate=DateTime.Now, UserId = 1, ProductId =1, Quantity =1}));

            //Act
            var controller = new OutboundController(mockRepository.Object);

            // Act
            var actionResult = controller.CreateCustomerOrder(data);
            var contentResult = (actionResult.Result as CreatedAtActionResult).Value as CustomerOrder;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Id);
        }

        [TestMethod]
        public void SetOrderStatusWillUpdateStatus()
        {
            // Arrange
            var mockRepository = new Mock<IOutboundRepository>();
            OrderStatus status = new OrderStatus()
            {
                Status = "Accepted",
                OrderId = 1,
            };
            bool? result = true;
            mockRepository.Setup(x => x.UpdateOrderStatus(status))
                .Returns(Task.FromResult(result));

            //Act
            var controller = new OutboundController(mockRepository.Object);

            // Act
            var actionResult = controller.SetOrderStatus(status);
            var contentResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Order status Updated", contentResult.Value);
        }

        [TestMethod]
        public void SetOrderQuantityWillUpdateQuantity()
        {
            // Arrange
            var mockRepository = new Mock<IOutboundRepository>();
            OrderQuantity quantity = new OrderQuantity()
            {
                Quantity = 10,
                OrderId = 1,
            };
            bool? result = true;
            mockRepository.Setup(x => x.UpdateOrderQuantity(quantity))
                .Returns(Task.FromResult(result));

            //Act
            var controller = new OutboundController(mockRepository.Object);
            var actionResult = controller.SetOrderQuantity(quantity);
            var contentResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Order quantity Updated", contentResult.Value);
        }


        [TestMethod]
        public void SetOrderDiscountWillUpdateDiscount()
        {
            // Arrange
            var mockRepository = new Mock<IOutboundRepository>();
            OrderDiscount discount = new OrderDiscount()
            {
                DiscountId = 10,
                OrderId = 1,
            };
            bool? result = true;
            mockRepository.Setup(x => x.UpdateOrderDiscount(discount))
                .Returns(Task.FromResult(result));

            //Act
            var controller = new OutboundController(mockRepository.Object);
            var actionResult = controller.SetOrderDiscount(discount);
            var contentResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Order discount Updated", contentResult.Value);
        }

        [TestMethod]
        public void CreateOrderShipmentWillCreateData()
        {
            // Arrange
            var mockRepository = new Mock<IOutboundRepository>();
            OrderShipmentData data = new OrderShipmentData()
            {
               CustomerOrderId = 1,
               DriverId = 1,
               TruckId =1,
            };

            mockRepository.Setup(x => x.CreateOrderShipment(data))
                .Returns(Task.FromResult(new OrderShipment() { CustomerOrderId =1, Id = 5, DriverDetailId = 1, TruckDetailId = 1, Status="Shipped"}));

            //Act
            var controller = new OutboundController(mockRepository.Object);

            var actionResult = controller.CreateOrderShipment(data);
            var contentResult = (actionResult.Result as CreatedAtActionResult).Value as OrderShipment;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Id);
        }

        [TestMethod]
        public void SetShipmentDateWillUpdateDate()
        {
            // Arrange
            var mockRepository = new Mock<IOutboundRepository>();
            ShipmentData data = new ShipmentData()
            {
              ShipmentDate = DateTime.Now,
              ShipmentId = 1
            };
            bool? result = true;
            mockRepository.Setup(x => x.UpdateShipmentDate(data))
                .Returns(Task.FromResult(result));

            //Act
            var controller = new OutboundController(mockRepository.Object);
            var actionResult = controller.SetShipmentDate(data);
            var contentResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Shipment schedule Updated", contentResult.Value);
        }

        [TestMethod]
        public void SetShipmentStatusWillUpdateStatus()
        {
            // Arrange
            var mockRepository = new Mock<IOutboundRepository>();
            ShipmentStatus data = new ShipmentStatus()
            {
                Status = "InProgress",
                ShipmentId = 1
            };
            bool? result = true;
            mockRepository.Setup(x => x.UpdateShipmentStatus(data))
                .Returns(Task.FromResult(result));

            //Act
            var controller = new OutboundController(mockRepository.Object);
            var actionResult = controller.SetShipmentStatus(data);
            var contentResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Shipment status Updated", contentResult.Value);
        }
    }
}
