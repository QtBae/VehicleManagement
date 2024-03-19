using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class VehicleDetail
    {
        [Parameter]
        public Guid Id { get; set; }

        private VehicleModel Vehicle;

        [Inject]
        public IVehicleServices VehicleService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        override protected async Task OnInitializedAsync()
        {
            var data = await VehicleService.GetVehicleByIdAsync(Id);
            if (data != null)
            {
                Vehicle = data;
            }
        }

        public void NavigateToEditMaintance(Guid id)
        {
            NavigationManager.NavigateTo($"/editmaintenances/{id}");
        }
    }
}
