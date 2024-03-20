using Bogus;
using Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SeedData
{
    public class BrandData
    {
        public static IEnumerable<BrandModel> GetBrands()
        {
            var faker = new Faker<BrandModel>()
                .RuleFor(b => b.Id,f => f.Random.Guid())
                .RuleFor(b => b.BrandName,f => f.Vehicle.Type());

            return faker.Generate(10);
        }
    }
}
