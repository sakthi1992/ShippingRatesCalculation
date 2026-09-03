using ShippingRatesCalculation.Dto;
using ShippingRatesCalculation.Interface;

namespace ShippingRatesCalculation.Services
{
    public class ShipmentRateService : IShipmentRateService
    {
        public ShipmentResponse CalculateRate(ShipmentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");
            }

            // Business validation
            if (request.Weight <= 0 || request.Weight > 70)
            {
                throw new ArgumentException("Weight must be greater than 0 and less than or equal to 70.");
            }

            if (request.Length <= 0 ||
                request.Width <= 0 ||
                request.Breadth <= 0)
            {
                throw new ArgumentException(
                    "Length, Width and Breadth must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(request.ServiceType))
            {
                throw new ArgumentException("ServiceType must be Standard or Express.");
            }

            if (string.IsNullOrWhiteSpace(request.DestinationZone))
            {
                throw new ArgumentException("DestinationZone must be Local, Regional or International.");
            }

            // Validate service type
            decimal baseRate = request.ServiceType.Trim().ToLowerInvariant() switch
            {
                "standard" => 5m,
                "express" => 8m,
                _ => throw new ArgumentException(
                    "ServiceType must be Standard or Express.")
            };

            // Validate destination zone
            decimal zoneMultiplier = request.DestinationZone.Trim().ToLowerInvariant() switch
            {
                "local" => 1.0m,
                "regional" => 1.5m,
                "international" => 2.5m,
                _ => throw new ArgumentException(
                    "DestinationZone must be Local, Regional or International.")
            };

            // Calculate volumetric weight
            decimal volumetricWeight =
                (request.Length * request.Width * request.Breadth) / 5000m;

            // Billable weight
            decimal billableWeight =
                Math.Max(request.Weight, volumetricWeight);

            // Final price
            decimal finalPrice =
                billableWeight * baseRate * zoneMultiplier;

            return new ShipmentResponse
            {
                BillableWeight = Math.Round(billableWeight, 2),
                FinalPrice = Math.Round(finalPrice, 2),

                PriceBreakdown = new PriceBreakdown
                {
                    BaseRate = baseRate,
                    ZoneMultiplier = zoneMultiplier
                }
            };
        }
    }
}
