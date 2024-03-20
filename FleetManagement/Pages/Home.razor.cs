using FleetManagement.ClientServices;
using FleetManagement.Data;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Pages
{
    public partial class Home
    {
        [Inject]
        public IVehicleServices VehicleServices { get; set; }

        public IEnumerable<VehicleModel?> Vehicles { get; set; }



        protected override void OnInitialized()
        {
            Vehicles = VehicleData.Instance.VehicleModels;
        }
    }
}
