using Microsoft.AspNetCore.Mvc;
using ShippingRatesCalculation.Dto;
using ShippingRatesCalculation.Interface;

namespace ShippingRatesCalculation.Controllers
{
    [ApiController]
    [Route("api/rates")]
    public class ShipmentRatesController : ControllerBase
    {
        private readonly IShipmentRateService _shipmentRateService;

        public ShipmentRatesController(
            IShipmentRateService shipmentRateService)
        {
            _shipmentRateService = shipmentRateService;
        }

        [HttpGet("calculate")]
        public IActionResult Calculate([FromQuery] ShipmentRequest request)
        {
            try
            {
                var response = _shipmentRateService.CalculateRate(request);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while calculating the shipment rate.",
                    detail = ex.Message
                });
            }
        }
    }
}
