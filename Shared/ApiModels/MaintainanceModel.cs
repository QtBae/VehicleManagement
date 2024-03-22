using System.ComponentModel.DataAnnotations;

namespace Shared.ApiModels
{
    public class MaintainanceModel
    {
        public Guid Id { get; set; }

        [Required (ErrorMessage = "The works are required")]
        public string Works { get; set; }

        [Required (ErrorMessage = "The mileage is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The mileage value must be positive")]
        public int Mileage { get; set; }

        [Required (ErrorMessage = "The date is required")]
        [DataType(DataType.Date , ErrorMessage = "The date is not valid")]
        public DateTime Date { get; set; }

        [Required (ErrorMessage = "The vehicle is required")]
        public Guid VehicleId { get; set; }
    }
}
