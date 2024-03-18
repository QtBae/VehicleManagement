using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    public class CarModel
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The model name should be atleast 1 charater")]
        public string ModelName { get; set; }
        public BrandModel Brand { get; set; }
        public int MaintenanceFrequency { get; set; }
    }
}
