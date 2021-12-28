/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotManagement.Tests
{
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using DepotManagement.Models;
    using DepotManagement.Repository;
    using System;
    using DepotDatabase;
    using DepotManagement.Controllers;
    using Microsoft.AspNetCore.Mvc;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateProductTypeWillCreateType()
        {
            // Arrange
            var mockRepository = new Mock<IManagementRepository>();
            ProductTypeData data = new ProductTypeData()
            {
                Type = "Clothes",
            };

            mockRepository.Setup(x => x.AddProductType(data))
                .Returns(Task.FromResult(new ProductType() { Id =5, Type = data.Type }));

            //Act
            var controller = new ManagementController(mockRepository.Object);

            var actionResult = controller.AddProductType(data);
            var contentResult = (actionResult.Result as CreatedAtActionResult).Value as ProductType;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Id);
        }

        [TestMethod]
        public void CreatePalletQuantityWillCreatePalletQuantity()
        {
            // Arrange
            var mockRepository = new Mock<IManagementRepository>();
            PalletQuantityData data = new PalletQuantityData()
            {
                MaxQuantity = 100,
                ProductTypeId = 1,
            };

            mockRepository.Setup(x => x.AddPalletQuantity(data))
                .Returns(Task.FromResult(new PalletQuantity() { Id = 5, MaxQuantity = data.MaxQuantity, ProductTypeId = data.ProductTypeId}));

            //Act
            var controller = new ManagementController(mockRepository.Object);

            var actionResult = controller.AddPalletQuantity(data);
            var contentResult = (actionResult.Result as CreatedAtActionResult).Value as PalletQuantity;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Id);
        }

        [TestMethod]
        public void CreateNodesWillCreateNode()
        {
            // Arrange
            var mockRepository = new Mock<IManagementRepository>();
            NodesData data = new NodesData()
            {
               Name = "Node 1",
               Location = "South",
               CreatedDate = DateTime.Now,
            };

            mockRepository.Setup(x => x.AddNodes(data))
                .Returns(Task.FromResult(new LPN() { Id = 5, AddedDate = data.CreatedDate , Nodes = new Nodes() { Id = 1, Location = data.Location, Name = data.Name } }));

            //Act
            var controller = new ManagementController(mockRepository.Object);

            var actionResult = controller.AddNodes(data);
            var contentResult = (actionResult.Result as CreatedAtActionResult).Value as LPN;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Id);
        }

        [TestMethod]
        public void CreatePalletItemTypeWillCreatePallet()
        {
            // Arrange
            var mockRepository = new Mock<IManagementRepository>();
            PalletsData data = new PalletsData()
            {
               LPNId = 1,
               PalletsQuantityId = 1,
               Quantity = 5
            };

            mockRepository.Setup(x => x.AddPalletItemType(data))
                .Returns(Task.FromResult( new Pallet() { Id = 5, Priority = "Low", LPNId = (int)data.LPNId, PalletQuantityId = data.PalletsQuantityId, Quantity = data.Quantity}));

            //Act
            var controller = new ManagementController(mockRepository.Object);

            var actionResult = controller.AddPalletItemType(data);
            var contentResult = (actionResult.Result as CreatedAtActionResult).Value as Pallet;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(5, contentResult.Id);
        }

        [TestMethod]
        public void AssignPalletLpnWillUpdatePallet()
        {
            // Arrange
            var mockRepository = new Mock<IManagementRepository>();
            PalletsLPNData data = new PalletsLPNData()
            {
               LpnId = 1,
               PalletId = 1,
            };
            bool? result = true;
            mockRepository.Setup(x => x.GetLpn(1))
               .Returns(Task.FromResult(new LPN { Id = 1, NodeId = 1, AddedDate = DateTime.Now}));

            mockRepository.Setup(x => x.UpdatePallet(data))
                .Returns(Task.FromResult(result));

            //Act
            var controller = new ManagementController(mockRepository.Object);

            // Act
            var actionResult = controller.AssignPalletLpn(1, data);
            var contentResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.AreEqual("Pallet Updated", contentResult.Value);
        }

        [TestMethod]
        public void AssignPalletLpnWillReturnBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IManagementRepository>();
            PalletsLPNData data = new PalletsLPNData()
            {
                LpnId = 3,
                PalletId = 1,
            };

            mockRepository.Setup(x => x.GetLpn(1))
               .Returns(Task.FromResult(new LPN { Id = 1, NodeId = 1, AddedDate = DateTime.Now }));

           
            //Act
            var controller = new ManagementController(mockRepository.Object);
            var actionResult = controller.AssignPalletLpn(1, data);
            var contentResult = actionResult.Result as BadRequestObjectResult;

            // Assert
            Assert.AreEqual("LPN ID mismatch", contentResult.Value);
        }

        [TestMethod]
        public void AssignPalletLpnWillReturnLpnNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IManagementRepository>();
            PalletsLPNData data = new PalletsLPNData()
            {
                LpnId = 1,
                PalletId = 1,
            };

            mockRepository.Setup(x => x.GetLpn(1))
               .Returns(Task.FromResult((LPN)null));

            //Act
            var controller = new ManagementController(mockRepository.Object);

            var actionResult = controller.AssignPalletLpn(1, data);
            var contentResult = actionResult.Result as NotFoundObjectResult;

            // Assert
            Assert.AreEqual("LPN with Id = 1 not found", contentResult.Value);
        }
    }
}
