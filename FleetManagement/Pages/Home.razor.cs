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

        public IEnumerable<VehicleModel?> Vehicles { get; set; }= new List<VehicleModel?>();


        protected override async Task OnInitializedAsync()
        {
            Vehicles = (await VehicleServices.GetAllVehiclesAsync()).AsQueryable().Where(v => v.Lateness < 0);
        }
    }
}
