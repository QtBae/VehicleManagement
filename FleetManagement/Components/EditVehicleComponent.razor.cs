using Blazorise;
using FleetManagement.ClientServices;
using FleetManagement.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class EditVehicleComponent
    {
        [Inject]
        public IVehicleServices VehicleServices { get; set; }

        [Inject]
        public IModelServices ModelServices { get; set; }
        [Inject]
        public IBrandService BrandService { get; set; }

        [Parameter]
        public VehicleModel Vehicle { get; set; }

        [Parameter]
        public IStringLocalizer Localizer { get; set; }
        [Parameter]
        public Modal ModalRef { get; set; }

        [Parameter]
        public EventCallback OnSave { get; set; }


        public Guid SelectedBrandId
        {
            get
            {
                return Vehicle.BrandId;
            }

            set
            {
                Vehicle!.BrandId = value;

            }
        }
        public Guid SelectedModelId
        {
            get=> Vehicle.ModelId;
            set
            {
                Vehicle.ModelId = value;
            }
        }

        //public Guid SelectedBrand
        //{
        //    get => Model.BrandId;
        //    set => Model.BrandId = value;
        //}

        private async Task SaveVehicle()
        {
            if (Vehicle.Id == Guid.Empty)
            {
                var response = await VehicleServices.AddVehicleAsync(Vehicle);
                if (response != null)
                {
                    await VehicleServices.GetAllVehiclesAsync();
                }
            }
            else
            {
                Vehicle.Model = null;
                Vehicle.Brand = null;   
                await VehicleServices.UpdateVehicleAsync(Vehicle);
            }
            await OnSave.InvokeAsync();
        }

        override protected async Task OnInitializedAsync()
        {
            Vehicle = Vehicle ?? new VehicleModel();
            Models = await ModelServices.GetModelsAsync();
            Brands = await BrandService.GetAllBrandsAsync();
            await base.OnInitializedAsync();

        }

        public Energy SelectedEnergy
        {
            get => Vehicle.Energy;
            set => Vehicle.Energy = value;
        }


        IEnumerable<BrandModel> Brands = new List<BrandModel>();
        IEnumerable<CarModel?> Models = new List<CarModel?>();
        List<Energy> Energies = Enum.GetValues<Energy>().ToList();
    }
}
