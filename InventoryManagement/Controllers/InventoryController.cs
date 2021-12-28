/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InventoryManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DepotDatabase;
    using InventoryManagement.Models;
    using InventoryManagement.Repository;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NLog;

    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IInventoryRepository inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        /// <summary>
        /// Api to get the pallet details
        /// </summary>
        /// <param name="filter">input parameter</param>
        /// <returns>returns collection</returns>
        [HttpGet]
        [Route("getpalletdetails")]
        public async Task<ActionResult<IEnumerable<PalletSearchResult>>> GetPalletDetails([FromBody] SearchFilter filter)
        {
            try
            {
                return (await inventoryRepository.GetPalletData(filter)).ToList();
            }
            catch (Exception)
            {
                logger.Error("API: GetPalletDetails Error retrieving data from the database");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Api to get all the orders
        /// </summary>
        /// <param name="filter">input parameter for the api</param>
        /// <returns>returns collection</returns>
        [HttpGet]
        [Route("getallorders")]
        public async Task<ActionResult<IEnumerable<OrderSearchResult>>> GetAllOrders([FromBody] SearchFilter filter)
        {
            try
            {
                return (await inventoryRepository.GetAllOrders(filter)).ToList();
            }
            catch (Exception)
            {
                logger.Error("API: GetAllOrders Error retrieving data from the database");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Api to get the search products
        /// </summary>
        /// <param name="productName">input parameter</param>
        /// <returns>returns collection</returns>
        [HttpGet]
        [Route("searchproducts")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string productName)
        {
            try
            {
                return (await inventoryRepository.SearchProduct(productName)).ToList();
            }
            catch (Exception)
            {
                logger.Error("API: SearchProducts Error retrieving data from the database");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Api to get the product details
        /// </summary>
        /// <param name="productId">input parameter</param>
        /// <returns>return product data</returns>
        [HttpGet]
        [Route("getproductdetails")]
        public async Task<ActionResult<ProductSearchResult>> GetProductDetails(int productId)
        {
            try
            {
                return (await inventoryRepository.GetProductDetails(productId));
            }
            catch (Exception)
            {
                logger.Error("API: GetProductDetails Error retrieving data from the database");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Api to get the product location
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns>returns location</returns>
        [HttpGet("{productId:int}")]
        [Route("getproductlocation")]
        public ActionResult GetProductLocation(int productId)
        {
            try
            {
                return Ok(inventoryRepository.GetProductLocation(productId));
            }
            catch (Exception)
            {
                logger.Error("API: GetProductLocation Error retrieving data from the database");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Api to get the order status
        /// </summary>
        /// <param name="orderId">order id</param>
        /// <returns>returns order status</returns>
        [HttpGet("{orderId:int}")]
        [Route("getorderstatus")]
        public async Task<ActionResult> GetOrderStatus(int orderId)
        {
            try
            {
                return Ok(await inventoryRepository.GetOrderStatus(orderId));
            }
            catch (Exception)
            {
                logger.Error("API: GetOrderStatus Error retrieving data from the database");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Api to get the product information
        /// </summary>
        /// <param name="model">product object</param>
        /// <returns>returns message</returns>
        [HttpPut]
        [Route("updateproductinformation")]
        public async Task<ActionResult> UpdateProductInformation([FromBody] Product model)
        {
            try
            {
                var result = await inventoryRepository.UpdateProductInformation(model);
                return result == null ? BadRequest("Error updating product information") : Ok("Product Updated");
            }
            catch (Exception)
            {
                logger.Error("API: UpdateProductInformation Error retrieving data from the database");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
    }
}
