using Blazorise;
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
        public IMaintenanceService MaintenanceService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public MaintainanceModel Maintenance { get; set; }

        private Modal _modalRef;

        override protected async Task OnInitializedAsync()
        {
            Maintenance = new MaintainanceModel();
            var data = await VehicleService.GetVehicleByIdAsync(Id);
            if (data != null)
            {
                Vehicle = data;
            }
        }

        private async Task SaveMaintainance()
        {
            var data =await MaintenanceService.CreateMaintainanceModelAsync(Maintenance);
            Vehicle = await VehicleService.GetVehicleByIdAsync(Id);
            Maintenance = new MaintainanceModel();
            await _modalRef.Hide();
        }

        private async Task AddMaintance()
        {
            Maintenance = new MaintainanceModel
            {
                VehicleId = Id,
                Date = DateTime.Now,
                Mileage = Vehicle.Mileage
            };
            await _modalRef.Show();
        }

        //public void NavigateToEditMaintance(Guid id)
        //{
        //    NavigationManager.NavigateTo($"/editmaintenances/{id}");
        //}

        //public void AddMaintance()
        //{
        //    NavigationManager.NavigateTo($"/editmaintenances");
        //}
    }
}
