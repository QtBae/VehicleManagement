using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Entities
{
    public class VehicleEntity
    {
        public Guid Id { get; set; }
        public CarEntity? Model { get; set; }

        public Guid ModelId { get; set; }
        public List<MaintainanceEntity>? Maintainances { get; set; }
        public BrandEntity? Brand { get; set; }

        public Guid BrandId { get; set; }
        public string LicensePlate { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public String Energy { get; set; }

    }
}
