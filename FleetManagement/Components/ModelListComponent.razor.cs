using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class ModelListComponent
    {
        private List<CarModel> _models = [];
        
        protected override async Task OnInitializedAsync()
        {
            if (ModelService != null) _models = await ModelService.GetModelsAsync();
        }
    }
}
