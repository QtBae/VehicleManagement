using Microsoft.AspNetCore.Components;
using Shared.ApiModels;

namespace FleetManagement.Components
{
    public partial class EditMaintenances
    {
        [CascadingParameter]
        public VehicleModel Vehicle { get; set; } = new VehicleModel();

        private MaintainanceModel _newMaintenance = new MaintainanceModel();

        private void AddMaintenance()
        {
            if (Vehicle.Maintainances == null)
            {
                Vehicle.Maintainances = [];
            }
            Vehicle.Maintainances.Add(_newMaintenance);
        }
    }
}
