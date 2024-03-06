using Blazorise;
using FleetManagement.Data;
using Shared.ApiModels;

namespace FleetManagement.Pages
{
    public partial class VehicleList
    {


        private void AddVehicle(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            modalRef.Show();
        }

        private void SaveVehicle()
        {
            modalRef.Hide();
        }

        private Modal modalRef;

        VehicleModel Vehicle = new VehicleModel();
        IEnumerable<BrandModel> Brands = VehicleData.GetBrands(10);
        IEnumerable<CarModel> Cars = VehicleData.GetCarsModels(10);

    }
}
