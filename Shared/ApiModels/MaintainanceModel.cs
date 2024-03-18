using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    public class MaintainanceModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Works done")]
        public string Works { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public VehicleModel Vehicle { get; set; }
    }
}
