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
        public int Id { get; set; }

        [Required]
        public CarModel Car { get; set; }

        List<MaintainanceModel>? Maintainances { get; set; }

        [Required]
        public BrandModel Brand { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "License plate must be at least 7 characters long")]
        [MaxLength(9, ErrorMessage = "License plate must be at most 9 characters long")]
        public string LicensePlate { get; set; }

        public int Year { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Mileage { get; set; }
    }
}
