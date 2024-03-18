using Blazorise;
using FleetManagement.Data;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Pages
{
    public partial class VehicleList
    {
        private string? _errorMessage;
        private string? _successMessage;
        public Guid SelectedBrandId
        {
            get => Vehicle.Brand?.Id ?? Guid.Empty;
            set
            {
                Vehicle!.Brand = Brands.FirstOrDefault(b => b.Id == value);

            }
        }
        public Guid SelectedCarId
        {
            get
            {
                if (Vehicle.Model != null) return Vehicle.Brand is not null ? Vehicle.Model.Id : Guid.Empty;
                return Guid.Empty;
            }
            set
            {
                Vehicle!.Model = Cars.FirstOrDefault(c => c.Id == value);
            }
        }

        private async Task HandleValidSubmit()
        {
            _successMessage = "Vehicle saved successfully!";
        }

        private void HandleInvalidSubmit()
        {
            _errorMessage = "Please correct the validation errors.";
        }

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
        CarModel car;
        IEnumerable<BrandModel> Brands = VehicleData.GetBrands(10);
        IEnumerable<CarModel?> Cars = VehicleData.GetCarsModels(10);

        protected override void OnInitialized()
        {

        }

    }

}
