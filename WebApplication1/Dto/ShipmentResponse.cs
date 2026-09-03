namespace ShippingRatesCalculation.Dto
{
    public class ShipmentResponse
    {
        public decimal BillableWeight { get; set; }

        public decimal FinalPrice { get; set; }

        public PriceBreakdown PriceBreakdown { get; set; } = new();
    }
}
