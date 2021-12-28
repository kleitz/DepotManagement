/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace OutboundService.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NLog;
    using OutboundService.Models;
    using OutboundService.Repository;

    /// <summary>
    /// Api controller for Outbound service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OutboundController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IOutboundRepository outboundRepository;

        public OutboundController(IOutboundRepository outboundRepository)
        {
            this.outboundRepository = outboundRepository;
        }

        /// <summary>
        /// Api to create customer order.
        /// </summary>
        /// <param name="model">api parameter object</param>
        /// <returns>returns created customer order</returns>
        [HttpPost]
        [Route("createcustomerorder")]
        public async Task<ActionResult> CreateCustomerOrder([FromBody] CustomerOrderData model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var createdCustomerOrder = await outboundRepository.CreateCustomerOrder(model);

                logger.Info("Success");
                return CreatedAtAction(nameof(CreateCustomerOrder),
                    new { id = createdCustomerOrder.Id }, createdCustomerOrder);
            }
            catch (Exception)
            {
                logger.Error("Error creating new customer order");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new customer order");
            }
        }

        /// <summary>
        /// Api to set order status
        /// </summary>
        /// <param name="model">api parameter object</param>
        /// <returns>returns message</returns>
        [HttpPut]
        [Route("setorderstatus")]
        public async Task<ActionResult> SetOrderStatus([FromBody] OrderStatus model)
        {
            try
            {
                var result = await outboundRepository.UpdateOrderStatus(model);
                var logMessage = result == null ? "Error on update" : "Success";
                logger.Info(logMessage);
                return result == null ? BadRequest("Error updating status") : Ok("Order status Updated");
            }
            catch (Exception)
            {
                logger.Error("Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        /// <summary>
        /// Api to update order quantity
        /// </summary>
        /// <param name="model">api parameter object</param>
        /// <returns>returns message</returns>
        [HttpPut]
        [Route("setorderquantity")]
        public async Task<ActionResult> SetOrderQuantity([FromBody] OrderQuantity model)
        {
            try
            {
                var result = await outboundRepository.UpdateOrderQuantity(model);
                var logMessage = result == null ? "Error on update" : "Success";
                logger.Info(logMessage);
                return result == null ? BadRequest("Error updating quantity") : Ok("Order quantity Updated");
            }
            catch (Exception)
            {
                logger.Error("Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        /// <summary>
        /// Api to update order discount
        /// </summary>
        /// <param name="model">api parameter object</param>
        /// <returns>returns message</returns>
        [HttpPut]
        [Route("setorderdiscount")]
        public async Task<ActionResult> SetOrderDiscount([FromBody] OrderDiscount model)
        {
            try
            {
                var result = await outboundRepository.UpdateOrderDiscount(model);
                var logMessage = result == null ? "Error on update" : "Success";
                logger.Info(logMessage);
                return result == null ? BadRequest("Error updating discount") : Ok("Order discount Updated");
            }
            catch (Exception)
            {
                logger.Error("Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        /// <summary>
        /// Api to create order shipment
        /// </summary>
        /// <param name="model">api parameter object</param>
        /// <returns>returns created order shipment</returns>
        [HttpPost]
        [Route("createcustomerorder")]
        public async Task<ActionResult> CreateOrderShipment([FromBody] OrderShipmentData model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var createdShipment = await outboundRepository.CreateOrderShipment(model);

                logger.Info("Success");
                return CreatedAtAction(nameof(CreateOrderShipment),
                    new { id = createdShipment.Id }, createdShipment);
            }
            catch (Exception)
            {
                logger.Error("Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new customer order shipment");
            }
        }

        /// <summary>
        /// Api to update the shipment schedule
        /// </summary>
        /// <param name="model">api parameter object</param>
        /// <returns>returns message</returns>
        [HttpPut]
        [Route("setshipmentdate")]
        public async Task<ActionResult> SetShipmentDate([FromBody] ShipmentData model)
        {
            try
            {
                var result = await outboundRepository.UpdateShipmentDate(model);
                var logMessage = result == null ? "Error on update" : "Success";
                logger.Info(logMessage);
                return result == null ? BadRequest("Error updating shipment schedule") : Ok("Shipment schedule Updated");
            }
            catch (Exception)
            {
                logger.Error("Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        /// <summary>
        /// Api to update the shipment status
        /// </summary>
        /// <param name="model">api parameter object</param>
        /// <returns>returns message</returns>
        [HttpPut]
        [Route("setshipmentstatus")]
        public async Task<ActionResult> SetShipmentStatus([FromBody] ShipmentStatus model)
        {
            try
            {
                var result = await outboundRepository.UpdateShipmentStatus(model);
                var logMessage = result == null ? "Error on update" : "Success";
                logger.Info(logMessage);
                return result == null ? BadRequest("Error updating shipment status") : Ok("Shipment status Updated");
            }
            catch (Exception)
            {
                logger.Error("Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
    }
}
