using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    internal class VehicleModel
    {
        public int Id { get; set; }
        public int modelId { get; set; }

        public int brandId { get; set; }

        public string licensePlate { get; set; }

        public int year { get; set; }

        public int mileage { get; set; }
    }
}
