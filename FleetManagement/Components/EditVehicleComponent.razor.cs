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
        #region inject

        [Inject]
        public IVehicleServices VehicleServices { get; set; }

        [Inject]
        public IModelServices ModelServices { get; set; }
        [Inject]
        public IBrandService BrandService { get; set; }

        #endregion


        #region parameters

        [Parameter]
        public VehicleModel Vehicle { get; set; }

        [Parameter]
        public IStringLocalizer Localizer { get; set; }
        [Parameter]
        public Modal ModalRef { get; set; }

        [Parameter]
        public EventCallback OnSave { get; set; }

        #endregion


        #region properties

        public Guid SelectedBrandId
        {
            get => Vehicle.BrandId;

            set => Vehicle!.BrandId = value;
        }
        public Guid SelectedModelId
        {
            get=> Vehicle.ModelId;
            set => Vehicle.ModelId = value;
        }
        public Energy SelectedEnergy
        {
            get => Vehicle.Energy;
            set => Vehicle.Energy = value;
        }

        #endregion
        
        #region methods

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
                await VehicleServices.UpdateVehicleAsync(Vehicle);
            }
            await OnSave.InvokeAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            Vehicle = Vehicle ?? new VehicleModel();
            _models = await ModelServices.GetModelsAsync();
            _brands = await BrandService.GetAllBrandsAsync();
            await base.OnInitializedAsync();

        }

       #endregion


        #region fields

       IEnumerable<BrandModel?> _brands = new List<BrandModel>();
       IEnumerable<CarModel?> _models = new List<CarModel?>();
       List<Energy> _energies = Enum.GetValues<Energy>().ToList();

       #endregion
    }
}
