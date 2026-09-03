using ShippingRatesCalculation.Dto;

namespace ShippingRatesCalculation.Interface
{
    public interface IShipmentRateService
    {
        ShipmentResponse CalculateRate(ShipmentRequest request);
    }
}
