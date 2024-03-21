using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        
        public CarModel? Model { get; set; }

        [Required]
        public Guid ModelId { get; set; }
        public List<MaintainanceModel>? Maintainances { get; set; }

        [Required]

        public Guid BrandId { get; set; }
        public BrandModel? Brand { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "License plate must be at least 7 characters long")]
        [MaxLength(9, ErrorMessage = "License plate must be at most 9 characters long")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "The year is required")]
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
                    return 0;
                }
                return Maintainances.OrderByDescending(m => m.Date).FirstOrDefault().Mileage + Model.MaintenanceFrequency - Mileage;
            }
            set { }
        }
    }
}
