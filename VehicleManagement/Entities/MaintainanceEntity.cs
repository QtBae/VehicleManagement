using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Entities
{
    public class MaintainanceEntity
    {
        public Guid Id { get; set; }
        public string Works { get; set; }
        public int Mileage { get; set; }
        public DateTime Date { get; set; }
        public CarEntity Vehicle { get; set; }
    }
}
