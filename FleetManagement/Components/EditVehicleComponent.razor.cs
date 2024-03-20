using FleetManagement.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared;
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
            get=> Vehicle.Model?.Id ?? Guid.Empty;
            //{
            //    if (Vehicle.Model != null)
            //        return Vehicle.Model is not null ? Vehicle.Model.Id : Guid.Empty;
            //    return Guid.Empty;
            //}
            set
            {
                Vehicle!.Model = Cars.FirstOrDefault(c => c.Id == value);
            }
        }

        public Energy SelectedEnergy
        {
            get => Vehicle.Energy;
            set => Vehicle.Energy = value;
        }


        IEnumerable<BrandModel> Brands = VehicleData.GetBrands(10);
        IEnumerable<CarModel?> Cars = VehicleData.GetCarsModels(10);
        List<Energy> Energies = Enum.GetValues<Energy>().ToList();
    }
}
