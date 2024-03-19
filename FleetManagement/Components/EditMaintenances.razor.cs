using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class EditMaintenances
    {
        [Inject]
        public MaintenanceService MaintenanceService { get; set; }
        [Parameter]
        public Guid VehicleId { get; set; }
        private VehicleModel Vehicle { get; set; }

        [Parameter]
        public Guid MaintenanceId { get; set; }
        private  MaintainanceModel Maintenance { get; set; }


        protected async  override Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            Vehicle = VehicleId != Guid.Empty ? await MaintenanceService.(VehicleId) : new VehicleModel();
        }

        //protected override Task OnParametersSetAsync()
        //{
        //    return base.OnParametersSetAsync();
        //}
    }
}
