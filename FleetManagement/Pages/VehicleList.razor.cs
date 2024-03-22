using Blazorise;
using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Pages
{
    public partial class VehicleList
    {
        [Inject]
        public IVehicleServices VehicleServices { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<VehicleModel?> Vehicles { get; set; }= new List<VehicleModel?>();
        
        private Modal _modalRef;

        VehicleModel Vehicle = new VehicleModel();



        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Vehicles = await VehicleServices.GetAllVehiclesAsync();
        }

        private async Task AddVehicle(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            await _modalRef.Show();
        }

        private async Task VehicleEdited()
        {
            
            await _modalRef.Hide();
            Vehicle= new VehicleModel();
            Vehicles = await VehicleServices.GetAllVehiclesAsync();
        }

        private void EditVehicle (VehicleModel vehicle)
        {
            Vehicle = vehicle;
            _modalRef.Show();
        }

        private async Task DeleteVehicle(VehicleModel vehicle)
        {
            var isDeleted=await VehicleServices.DeleteVehicleAsync(vehicle);
            if (isDeleted)
            {
                Vehicles = await VehicleServices.GetAllVehiclesAsync();
            }

        }

        private void ShowVehicleDetail(Guid id)
        {
            NavigationManager.NavigateTo($"/vehicledetail/{id}");
        }




    }

}
