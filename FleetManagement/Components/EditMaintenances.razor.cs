using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class EditMaintenances
    {
        [Inject]
        public IMaintenanceService MaintenanceService { get; set; }


        [Parameter]
        public Guid MaintenanceId { get; set; }
        private  MaintainanceModel Maintenance { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Maintenance = MaintenanceId == Guid.Empty ? new MaintainanceModel() : await MaintenanceService.GetMaintainanceModelByIdAsync(MaintenanceId) ?? new MaintainanceModel();
        }

        

    }
}
