using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class VehicleDetail
    {
        [CascadingParameter]
        public Guid Id { get; set; }

        private VehicleModel Vehicle;

        [Inject]
        public IVehicleServices VehicleService { get; set; }

        override protected async Task OnInitializedAsync()
        {
            var data = await VehicleService.GetVehicleByIdAsync(Id);
            if (data != null)
            {
                Vehicle = data;
            }
        }
    }
}
