/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace InboundService.Controllers
{
    using System;
    using System.Threading.Tasks;
    using InboundService.Models;
    using InboundService.Repository;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NLog;

    [Route("api/[controller]")]
    [ApiController]
    public class InboundController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IInboundRepository inboundRepository;

        public InboundController(IInboundRepository inboundRepository)
        {
            this.inboundRepository = inboundRepository;
        }

        /// <summary>
        /// Api to create the inbound order
        /// </summary>
        /// <param name="model">input parameter</param>
        /// <returns>returns object</returns>
        [HttpPost]
        [Route("createinboundorder")]
        public async Task<ActionResult> CreateInboundOrder([FromBody] InboundData model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var createdInboundOrder = await inboundRepository.CreateInboundOrder(model);

                logger.Info("Success: API CreateInboundOrder");
                return CreatedAtAction(nameof(CreateInboundOrder),
                    new { id = createdInboundOrder.Id }, createdInboundOrder);
            }
            catch (Exception)
            {
                logger.Error("API: CreateInboundOrder Error creating new inbound order");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new inbound order");
            }
        }

        /// <summary>
        /// Api to create pallet item
        /// </summary>
        /// <param name="inboundOrderId">input parameter inbound id</param>
        /// <returns>returns product</returns>
        [HttpPost]
        [Route("assignpalletitem")]
        public async Task<ActionResult> AssignPalletItem(int inboundOrderId)
        {
            try
            {
                if (inboundOrderId == 0)
                    return BadRequest();

                var createdProduct = await inboundRepository.AssignPalletItem(inboundOrderId);

                logger.Info("Success: API AssignPalletItem");
                return CreatedAtAction(nameof(AssignPalletItem),
                    new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception)
            {
                logger.Error("API: AssignPalletItem Error assigning pallet item");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error assigning pallet item");
            }
        }

        /// <summary>
        /// Api to reassign pallet item
        /// </summary>
        /// <param name="model">input parameter object</param>
        /// <returns>returns message</returns>
        [HttpPut]
        [Route("reassignpalletitem")]
        public async Task<ActionResult> ReassignPalletItem([FromBody] ReassignData model)
        {
            try
            {              
                var result = await inboundRepository.ReassignPalletItem(model);
                return result == null ? BadRequest("Error reassigning pallet item/product") : Ok("Pallet item/product Updated");
            }
            catch (Exception)
            {
                logger.Error("API: ReassignPalletItem Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        /// <summary>
        /// Api to get the quantity
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>returns message</returns>
        [HttpGet("{id:int}")]
        [Route("getitemquantity")]
        public  IActionResult GetItemQuantity(int id)
        {
            try
            {
                var result = inboundRepository.GetItemQuantity(id);

                if (result == 0) return NotFound();

                logger.Info("Success: API GetItemQuantity");
                return Ok(result);
            }
            catch (Exception)
            {
                logger.Error("API: GetItemQuantity Error retrieving data from the database");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
