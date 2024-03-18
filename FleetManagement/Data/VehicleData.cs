using Bogus;
using Shared;
using Shared.ApiModels;

namespace FleetManagement.Data
{
    public abstract class VehicleData
    {
        private static IEnumerable<CarModel?> _carModels= new List<CarModel?>();

        public static IEnumerable<VehicleModel?> GetVehicles()
        {
            _carModels=GetCarsModels();
            var brands = GetBrands();

            var faker = new Faker<VehicleModel>()
                .RuleFor(v => v.Id, f => f.Random.Guid())
                .RuleFor(v => v.Model, f => f.PickRandom(_carModels))
                .RuleFor(v => v.Brand, f => f.PickRandom(brands))
                .RuleFor(v => v.LicensePlate, f => f.Vehicle.Vin())
                .RuleFor(v => v.Year, f => f.Random.Int(2000, 2020))
                .RuleFor(v => v.Mileage, f => f.Random.Int(0, 100000))
                .RuleFor(b => b.Energy,f => f.PickRandom<Energy>());

            return faker.Generate(100);
        }


        public static IEnumerable<CarModel?> GetCarsModels(int nbr=100)
        {
               var faker = new Faker<CarModel>()
                .RuleFor(c => c.Id, f => f.Random.Guid())
                .RuleFor(b => b.MaintenanceFrequency,f => f.Random.Int(1,100))
                .RuleFor(c => c.ModelName, f => f.Vehicle.Model())
                .RuleFor(c => c.MaintenanceFrequency, f => f.Random.Int(1, 100));

            return faker.Generate(nbr);
        }

        public static IEnumerable<BrandModel> GetBrands(int nbr=100)
        {
            _carModels=GetCarsModels();
            var faker = new Faker<BrandModel>()
                .RuleFor(b => b.Id,f => f.Random.Guid())
                .RuleFor(b => b.BrandName,f => f.Vehicle.Manufacturer());

            return faker.Generate(nbr);
        }   
    }
}
