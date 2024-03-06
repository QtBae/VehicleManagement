using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    public class BrandModel
    {
        public int Id { get; set; }
        public CarModel Car { get; set; }
        public string BrandName { get; set; }
        public int MaintainanceFrequency { get; set; }
        public Energy Energy { get; set; }
    }
}
