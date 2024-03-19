using Blazorise;
using FleetManagement.Data;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Pages
{
    public partial class VehicleList
    {
        
        private void AddVehicle(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            _modalRef.Show();
        }

        private void SaveVehicle()
        {
            _modalRef.Hide();
        }

        private Modal _modalRef;

        VehicleModel Vehicle = new VehicleModel();

        protected override void OnInitialized()
        {

        }

    }

}
