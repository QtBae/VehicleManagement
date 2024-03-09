using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class VehicleListComponent
    {
        [Inject]
        public IVehicleServices VehicleServices { get; set; }

        public IEnumerable<VehicleModel> Vehicles { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Vehicles = await VehicleServices.GetAllVehiclesAsync();
        }
    }
}
