using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public CarModel Car { get; set; }

        public BrandModel Brand { get; set; }

        public string LicensePlate { get; set; }

        public int Year { get; set; }

        public int Mileage { get; set; }
    }
}
