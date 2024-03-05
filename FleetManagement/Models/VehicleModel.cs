namespace FleetManagement.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public CarModel Car { get; set; }

        public BrandModel Brand { get; set; }

        public string LicensePlate { get; set; }

        public int Year { get; set; }

        public int Mileage { get; set; }
    }
}
