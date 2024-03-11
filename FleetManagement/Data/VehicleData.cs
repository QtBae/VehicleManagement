using Bogus;
using Shared;
using Shared.ApiModels;

namespace FleetManagement.Data
{
    public class VehicleData
    {
        public static IEnumerable<CarModel> carModels= new List<CarModel>();

        public static IEnumerable<VehicleModel> GetVehicles()
        {
            carModels=GetCarsModels();
            var brands = GetBrands();

            var faker = new Faker<VehicleModel>()
                .RuleFor(v => v.Id, f => f.Random.Guid())
                .RuleFor(v => v.Car, f => f.PickRandom(carModels))
                .RuleFor(v => v.Brand, f => f.PickRandom(brands))
                .RuleFor(v => v.LicensePlate, f => f.Vehicle.Vin())
                .RuleFor(v => v.Year, f => f.Random.Int(2000, 2020))
                .RuleFor(v => v.Mileage, f => f.Random.Int(0, 100000));

            return faker.Generate(100);
        }


        public static IEnumerable<CarModel> GetCarsModels(int nbr=100)
        {
               var faker = new Faker<CarModel>()
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(c => c.Model, f => f.Vehicle.Model())
                .RuleFor(c => c.MaintenanceFrequency, f => f.Random.Int(1, 100));

            return faker.Generate(nbr);
        }

        public static IEnumerable<BrandModel> GetBrands(int nbr=100)
        {
            carModels=GetCarsModels();
            var faker = new Faker<BrandModel>()
                .RuleFor(b => b.Id, f => f.Random.Guid())
                .RuleFor(b => b.Car,f => f.PickRandom(carModels))
                .RuleFor(b => b.BrandName,f => f.Vehicle.Manufacturer())
                .RuleFor(b => b.MaintainanceFrequency,f => f.Random.Int(1,100))
                .RuleFor(b => b.Energy,f => f.PickRandom<Energy>());

            return faker.Generate(nbr);
        }   
    }
}
