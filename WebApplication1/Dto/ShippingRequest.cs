namespace ShippingRatesCalculation.Dto
{
    public class ShipmentRequest
    {
        public decimal Weight { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Breadth { get; set; }

        public decimal Height
        {
            get => Breadth;
            set => Breadth = value;
        }

        public string ServiceType { get; set; } = string.Empty;

        public string DestinationZone { get; set; } = string.Empty;
    }
}

