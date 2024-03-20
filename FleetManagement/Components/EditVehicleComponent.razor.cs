using FleetManagement.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class EditVehicleComponent
    {
        [Parameter]
        public VehicleModel Vehicle { get; set; }

        [Parameter]
        public IStringLocalizer Localizer { get; set; }


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
                if (Vehicle.Model != null)
                    return Vehicle.Brand is not null ? Vehicle.Model.Id : Guid.Empty;
                return Guid.Empty;
            }
            set
            {
                Vehicle!.Model = Cars.FirstOrDefault(c => c.Id == value);
            }
        }


        IEnumerable<BrandModel> Brands = VehicleData.GetBrands(10);
        IEnumerable<CarModel?> Cars = VehicleData.GetCarsModels(10);
    }
}
