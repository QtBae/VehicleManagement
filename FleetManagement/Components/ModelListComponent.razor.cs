using Blazorise;
using FleetManagement.ClientServices;
using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class ModelListComponent
    {
        #region fireld

        private List<CarModel?> _models = [];
        
        private List<BrandModel?> _brands = [];
        private Modal _modalRef;

        #endregion

        [Inject]
        public IBrandService? BrandService { get; set; }


        [Parameter]
        public CarModel? Model { get; set; }

        

        public Guid SelectedBrand
        {
            get => Model!.BrandId;
            set => Model!.BrandId = value;
        }


        #region methods

        protected override async Task OnInitializedAsync()
        {
            Model ??= new CarModel();
            if (ModelService is not null) _models = await ModelService.GetModelsAsync();
            if (BrandService is not null) _brands = (await BrandService.GetAllBrandsAsync()).ToList();
        }

        private async Task SaveModel()
        {
            if (Model!.Id == Guid.Empty)
            {
                var response = await ModelService.AddModel(Model);
                if (response is not null)
                {
                    _models.Add(response);
                }
            }
            else
            {
                await ModelService.UpdateModel(Model);
            }
            await _modalRef.Hide();
        }

        private void EditModel(Guid id)
        {
            Model = _models.FirstOrDefault(m => m.Id == id);
            _modalRef.Show();
        }



        private void AddModel()
        {
            Model = new CarModel();
            _modalRef.Show();
        }

        private async Task DeleteModel(Guid id)
        {
            await ModelService.DeleteModel(id);
            _models = await ModelService.GetModelsAsync();
        }

        #endregion
    }
}
