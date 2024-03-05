using FleetManagement.Data;
using FleetManagement.Models;

namespace FleetManagement.ClientServices
{
    public class VehicleServices: IVehicleServices
    {
        public VehicleServices() { }

        public IEnumerable<VehicleModel> GetAllVehicles()
        {
            return VehicleData.GetVehicles();
        }
    }

    public interface IVehicleServices
    {
        IEnumerable<VehicleModel> GetAllVehicles();
    }
}
