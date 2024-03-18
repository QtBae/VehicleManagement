using Blazorise;
using FleetManagement.Data;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Pages
{
    public partial class VehicleList
    {
        private string errorMessage;
        private string successMessage;
        public Guid SelectedBrandId
        {
            get => Vehicle.Brand is not null ? Vehicle.Brand.Id : Guid.Empty;
            set
            {
                Vehicle!.Brand = Brands.FirstOrDefault(b => b.Id == value);

            }
        }
        public Guid SelectedCarId
        {
            get=> Vehicle.Brand is not null ? Vehicle.Car.Id : Guid.Empty;
            set
            {
                Vehicle!.Car = Cars.FirstOrDefault(c => c.Id == value);
            }
        }

        private async Task HandleValidSubmit()
        {
            successMessage = "Vehicle saved successfully!";
        }

        private void HandleInvalidSubmit()
        {
            errorMessage = "Please correct the validation errors.";
        }

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
        CarModel car;
        IEnumerable<BrandModel> Brands = VehicleData.GetBrands(10);
        IEnumerable<CarModel> Cars = VehicleData.GetCarsModels(10);

        protected override void OnInitialized()
        {

        }

    }

}
