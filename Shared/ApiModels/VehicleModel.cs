using System.ComponentModel.DataAnnotations;

namespace Shared.ApiModels
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        
        public CarModel? Model { get; set; }

        [Required]
        [NotEmptyGuid (ErrorMessage = "The model is required")]
        public Guid ModelId { get; set; }
        public List<MaintainanceModel>? Maintainances { get; set; }

        [Required]
        [NotEmptyGuid(ErrorMessage = "The brand is required")]
        public Guid BrandId { get; set; }
        public BrandModel? Brand { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "License plate must be at least 7 characters long")]
        [MaxLength(9, ErrorMessage = "License plate must be at most 9 characters long")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "The year is required")]
        [Range(1900, 2100, ErrorMessage = "The year must be between 1900 and 2100")]
        public int Year { get; set; }

        [Required(ErrorMessage = "The mileage is required")]
        [Range(0, int.MaxValue,ErrorMessage = "The mileage value must be positive")]
        public int Mileage { get; set; }
        public Energy Energy { get; set; }

        public int? Lateness
        {
            get
            {
                if(Maintainances == null || !Maintainances.Any() )
                {
                    return Mileage - Model.MaintenanceFrequency;
                }
                return Maintainances.OrderByDescending(m => m.Date).FirstOrDefault().Mileage + Model.MaintenanceFrequency - Mileage;
            }
            set { }
        }
    }
}
