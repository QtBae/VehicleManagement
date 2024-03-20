using Blazorise;
using FleetManagement.ClientServices;
using FleetManagement.Data;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Pages
{
    public partial class VehicleList
    {
        [Inject]
        public IVehicleServices VehicleServices { get; set; }

        public IEnumerable<VehicleModel?> Vehicles { get; set; }



        protected override async Task OnInitializedAsync()
        {
            Vehicles = await VehicleServices.GetAllVehiclesAsync();
        }

        private void AddVehicle(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            _modalRef.Show();
        }

        private void EditVehicle (VehicleModel vehicle)
        {
            Vehicle = vehicle;
            _modalRef.Show();
        }

        private void SaveVehicle()
        {
            _modalRef.Hide();
        }

        private Modal _modalRef;

        VehicleModel Vehicle = new VehicleModel();




    }

}
