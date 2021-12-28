/// <summary>
/// Copyright 21.12.1
/// </summary>

namespace DepotManagement.Controllers
{
    using System;
    using System.Threading.Tasks;
    using DepotManagement.Models;
    using DepotManagement.Repository;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NLog;

    /// <summary>
    /// API's for the Depot System management
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IManagementRepository managementRepository;

        public ManagementController(IManagementRepository managementRepository)
        {
            this.managementRepository = managementRepository;
        }

        /// <summary>
        /// API to add product type for the depot system
        /// </summary>
        /// <param name="model">represents the object to store in producttype</param>
        /// <returns>returns created object on success</returns>
        [HttpPost]
        [Route("addproducttype")]
        public async Task<ActionResult> AddProductType([FromBody] ProductTypeData model)
        {
            try
            {
                if (model == null)
                {
                    logger.Error("Incorrect parameter");
                    return BadRequest();
                }
               
                var createdProductType = await managementRepository.AddProductType(model);

                logger.Info("Success: added product type ");
                return CreatedAtAction(nameof(AddProductType),
                    new { id = createdProductType.Id }, createdProductType);
            }
            catch (Exception)
            {
                logger.Error("API: AddProductType Error creating new product type");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new product type");
            }
        }

        /// <summary>
        /// API to add pallet quantity for the depot system
        /// </summary>
        /// <param name="model">represents the object to store in palletquantity</param>
        /// <returns>returns created object on success</returns>
        [HttpPost]
        [Route("addpalletquantity")]
        public async Task<ActionResult> AddPalletQuantity([FromBody] PalletQuantityData model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var createdPalletQuantity = await managementRepository.AddPalletQuantity(model);

                logger.Info("Success: added pallet quantity ");
                return CreatedAtAction(nameof(AddPalletQuantity),
                    new { id = createdPalletQuantity.Id }, createdPalletQuantity);
            }
            catch (Exception)
            {
                logger.Error("API: AddPalletQuantity Error creating new pallet quantity");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new pallet quantity");
            }
        }

        /// <summary>
        /// API to add nodes for the depot system
        /// </summary>
        /// <param name="model">represents the object to store in node</param>
        /// <returns>returns created object on success</returns>
        [HttpPost]
        [Route("addnodes")]
        public async Task<ActionResult> AddNodes([FromBody] NodesData model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var createdLPNWithNodes = await managementRepository.AddNodes(model);

                logger.Info("Success: API AddNodes");
                return CreatedAtAction(nameof(AddNodes),
                    new { id = createdLPNWithNodes.Id }, createdLPNWithNodes);
            }
            catch (Exception)
            {
                logger.Error("API: AddNodes Error creating new node");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new node");
            }
        }

        /// <summary>
        /// API to add pallet item for the depot system
        /// </summary>
        /// <param name="model">represents the object to store in pallet</param>
        /// <returns>returns created object on success</returns>
        [HttpPost]
        [Route("addpalletitemtype")]
        public async Task<ActionResult> AddPalletItemType([FromBody] PalletsData model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var createdPalletItem = await managementRepository.AddPalletItemType(model);

                logger.Info("Success: API AddPalletItemType");
                return CreatedAtAction(nameof(AddPalletItemType),
                    new { id = createdPalletItem.Id }, createdPalletItem);
            }
            catch (Exception)
            {
                logger.Error("API: AddPalletItemType Error creating new pallet item type");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new pallet item type");
            }
        }

        /// <summary>
        /// API to assign Lpn to the pallet in the depot system
        /// </summary>
        /// <param name="id">represents the Lpn Id</param>
        /// <param name="model">represents input data</param>
        /// <returns><returns message </returns>
        [HttpPut("{id}")]
        [Route("assignpalletlpn")]
        public async Task<ActionResult> AssignPalletLpn(int id, [FromBody] PalletsLPNData model)
        {
            try
            {
                if (id != model.LpnId)
                {
                    logger.Error("BadRequest: API AddPalletItemType");
                    return BadRequest("LPN ID mismatch");
                }

                var LpnToUpdate = await managementRepository.GetLpn(id);

                if (LpnToUpdate == null)
                {
                    logger.Error("BadRequest: API AddPalletItemType LPN with Id = {id} not found");
                    return NotFound($"LPN with Id = {id} not found");
                }

                var result = await managementRepository.UpdatePallet(model);
                return result == null? BadRequest("Error updating pallet") : Ok("Pallet Updated");
            }
            catch (Exception)
            {
                logger.Error("API: AssignPalletLpn Error updating data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

    }
}
