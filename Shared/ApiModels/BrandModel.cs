using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ApiModels
{
    internal class BrandModel
    {
        public int Id { get; set; }
        public int modelId { get; set; }
        public string brandName { get; set; }
        public int maintainanceFrequency { get; set; }
        public Energy energy { get; set; }
    }
}
