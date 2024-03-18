using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Entities
{
    public class CarEntity
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public BrandEntity Brand { get; set; }
        public int MaintenanceFrequency { get; set; }
    }
}
