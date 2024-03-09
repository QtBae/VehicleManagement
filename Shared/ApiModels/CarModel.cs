using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    public class CarModel
    {
        public Guid Id { get; set; }

        public string Model { get; set; }
        public int MaintenanceFrequency { get; set; }
    }
}
