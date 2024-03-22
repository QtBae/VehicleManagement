using System.ComponentModel.DataAnnotations;

namespace Shared.ApiModels
{
    public class CarModel
    {
        public Guid Id { get; set; }

        [Required (ErrorMessage = "The model name is required")]
        [MinLength(1, ErrorMessage = "The model name should be atleast 1 charater")]
        public string ModelName { get; set; }

        [Required]
        [NotEmptyGuid(ErrorMessage = "The brand is required")]
        public Guid BrandId { get; set; }
        public BrandModel? Brand { get; set; }
        public int MaintenanceFrequency { get; set; }
    }
}
