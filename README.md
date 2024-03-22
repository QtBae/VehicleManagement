![GitHub Logo](https://github.com/Jael-dev/VehicleManagement/assets/77239264/34a2c2f6-b29b-452c-ab77-e2a9f6b3d260)


# Car Management Dot Net App

---

## Liens Vers Application Hébergée

- Blazor:
- Api:

---

## Fonctionnalités

- **Détails de la Voiture** : Suivez tous les détails de votre voiture, du modèle, de la marque et de l'année au numéro de VIN et à la plaque d'immatriculation.
- **Plannings de Maintenance** : Cette application vous permet  de suivre les besoins de maintenance de votre voiture.
- **Interface Conviviale** : L'application est construite en utilisant Blazor, offrant une interface utilisateur fluide et interactive.

---

## Structure du projet

Notre solution est constitué de 4 projets principaux structurés comme suit

- FleetManagement: Ce projet est la partie visuelle de notre application. Les technologies utilisées sont dot net et blazor.
- Shared: Ce projet contient toutes les classes qui seront utilisées et partagées dans notre application.
- VehicleManagement: Ce projet contient toute la logique de l’application. Des controlleurs aux entités, tout est défini ici.
- VehicleManagementUnitTest: Ce projet contient tous les tests unitaires de notre projet.

<img width="403" alt="projstruct" src="https://github.com/Jael-dev/VehicleManagement/assets/77239264/25bd6989-252e-4392-be4d-e17c71714ed5">

### 1 - FleetManagement

<img width="400" alt="fleetstruct" src="https://github.com/Jael-dev/VehicleManagement/assets/77239264/651f2a29-2c96-4b70-8895-5eeafb0b0d5b">

### 2 - Shared

Ce dossier contient les differentes classes. Une classe est dédini ainsi:

```csharp
using System;

namespace Shared.ApiModels
{
    public class NomDeLaClasse
    {
        public TypeAttribut1 Attribut1 { get; set; }
        // Exemple
        public string BrandName { get; set; }
    }
}
```

### 3 - VehicleManagement

Les dossiers important de cette structure sont:

- Entities: Dans ce dossier nous mettons la liste des entités correspondant aux classes spécifiées dans le dossier shared.
    
    ```csharp
    // Exemple d'entité Brand
    
    namespace VehicleManagement.Entities
    {
        public class BrandEntity
        {
            public Guid Id { get; set; }
            public string BrandName { get; set; }
        }
    }
    
    ```
    
- Profiles: Dans ce dossier nous mettons la les des classes correspondant aux entités qui se feront mappés.
    
    ```csharp
    using Shared.ApiModels;
    using AutoMapper;
    namespace VehicleManagement.Profiles
    {
        public class BrandProfile : Profile
        {
            public BrandProfile()
            {
                CreateMap<Entities.BrandEntity, BrandModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName));
    
                CreateMap<BrandModel, Entities.BrandEntity>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName));
            }
        }
    }
    
    ```
    
- Repositories: De ce dossiers nous mettons la listes des fonctions liées aux CRUD pour chaque model.
    
    ```csharp
    using Microsoft.EntityFrameworkCore;
    using VehicleManagement.Data;
    using VehicleManagement.Entities;
    
    namespace VehicleManagement.Repositories
    {
        public class BrandRepository : IBrandRepository
        {
            private readonly AppDbContext _context;
    
            public BrandRepository(AppDbContext context)
            {
                _context = context;
            }
    
            public async Task<IEnumerable<BrandEntity>> GetAllBrandsAsync()
            {
                return await _context.Brands.ToListAsync();
            }
    
            public async Task<BrandEntity> GetBrandByIdAsync(Guid id)
            {
                return await _context.Brands.FindAsync(id);
            }
    
            public async Task<BrandEntity> CreateBrandAsync(BrandEntity brand)
            {
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return brand;
            }
    
            public async Task<BrandEntity> UpdateBrandAsync(BrandEntity brand)
            {
                //_context.Entry(brand).State = EntityState.Modified; // this is another way to update
                _context.Brands.Update(brand); // this is another way to update
                await _context.SaveChangesAsync();
                return brand;
            }
    
            public async Task<bool> DeleteBrandAsync(Guid id)
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    return false;
                }
    
                var result = _context.Brands.Remove(brand);
                if (result.State == EntityState.Deleted)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        
    }
    
        public interface IBrandRepository
        {
            // get all brands
            Task<IEnumerable<BrandEntity>> GetAllBrandsAsync();
    
            // get  brands by id
            Task<BrandEntity> GetBrandByIdAsync(Guid id);
    
            // create brand
            Task<BrandEntity> CreateBrandAsync(BrandEntity brand);
    
            // update brand
            Task<BrandEntity> UpdateBrandAsync(BrandEntity brand);
    
            // delete brand
            Task<bool> DeleteBrandAsync(Guid id);
    
        }
    }
    
    ```
    
- Services: Dans les fichiers services, nous implementons toutes les methodes déclarées dans les repositories.
    
    ```csharp
    using AutoMapper;
    using Shared.ApiModels;
    using VehicleManagement.Entities;
    using VehicleManagement.Repositories;
    
    namespace VehicleManagement.Services
    {
        public class BrandService : IBrandService
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly ILogger<BrandService> _logger;
    
            public BrandService(IBrandRepository brandRepository, IMapper mapper, ILogger<BrandService> logger)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _logger = logger;
            }
    
            public async Task<IEnumerable<BrandModel>> GetAllBrandsAsync()
            {
                var brands = await _brandRepository.GetAllBrandsAsync();
                if (brands == null)
                {
                    _logger.LogError("No brands found");
                    return null;
                }
    
                _logger.LogInformation("Brands found");
                return _mapper.Map<IEnumerable<BrandModel>>(brands);
            }
    
            public async Task<BrandModel> GetBrandByIdAsync(Guid id)
            {
                if (id <= null)
                {
                    _logger.LogError("Invalid brand id");
                    return null;
                }
    
                var brand = await _brandRepository.GetBrandByIdAsync(id);
                if (brand == null)
                {
                    _logger.LogError("Brand not found");
                    return null;
                }
    
                _logger.LogInformation("Brand found");
                return _mapper.Map<BrandModel>(brand);
            }
    
            public async Task<BrandModel> CreateBrandAsync(BrandModel brand)
            {
                if (brand == null)
                {
                    _logger.LogError("Invalid brand");
                    return null;
                }
    
                var brandEntity = _mapper.Map<BrandEntity>(brand);
                var createdBrand = await _brandRepository.CreateBrandAsync(brandEntity);
                if (createdBrand == null)
                {
                    _logger.LogError("Brand not created");
                    return null;
                }
    
                _logger.LogInformation("Brand created");
                return _mapper.Map<BrandModel>(createdBrand);
            }
    
            public async Task<BrandModel?> UpdateBrandAsync(BrandModel brand)
            {
                if (brand == null)
                {
                    _logger.LogError("Invalid brand");
                    return null;
                }
    
                var brandEntity = _mapper.Map<BrandEntity>(brand);
                var updatedBrand = await _brandRepository.UpdateBrandAsync(brandEntity);
                if (updatedBrand == null)
                {
                    _logger.LogError("Brand not updated");
                    return null;
                }
    
                _logger.LogInformation("Brand updated");
                return _mapper.Map<BrandModel>(updatedBrand);
            }
    
            public async Task<bool> DeleteBrandAsync(Guid id)
            {
                if (id <= null)
                {
                    _logger.LogError("Invalid brand id");
                    return false;
                }
    
                var deleted = await _brandRepository.DeleteBrandAsync(id);
                if (!deleted)
                {
                    _logger.LogError("Brand not deleted");
                    return false;
                }
    
                _logger.LogInformation("Brand deleted");
                return true;
            }
        }
    
        public interface IBrandService
        {
            Task<IEnumerable<BrandModel>> GetAllBrandsAsync();
    
            // get brand by id
            Task<BrandModel> GetBrandByIdAsync(Guid id);
    
            // create brand
            Task<BrandModel> CreateBrandAsync(BrandModel brand);
    
            // update brand
            Task<BrandModel> UpdateBrandAsync(BrandModel brand);
    
            // delete brand
            Task<bool> DeleteBrandAsync(Guid id);
    
        }
    }
    
    ```
    
- Controllers: Dans les fichiers controlleurs, nous définissons toutes les methodes déclarées dans les repositories.
    
    ```csharp
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Shared.ApiModels;
    using VehicleManagement.Services;
    
    namespace VehicleManagement.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BrandController : ControllerBase
        {
            private readonly IBrandService _brandService;
    
            public BrandController(IBrandService brandService)
            {
                _brandService = brandService;
            }
    
            /// <summary>
            /// Get all brands
            /// </summary>
            /// <returns> A list of brands if found or not found if no brands are found</returns>
    
            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> GetAllBrandsAsync()
            {
                var brands = await _brandService.GetAllBrandsAsync();
                if (brands == null)
                {
                    return NotFound();
                }
    
                return Ok(brands);
            }
    
            [HttpGet("{id}")]
            [ActionName("GetBrandByIdAsync")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> GetBrandByIdAsync(Guid id)
            {
                if (id == Guid.Empty)
                {
                    return BadRequest();
                }
    
                var brand = await _brandService.GetBrandByIdAsync(id);
                if (brand == null)
                {
                    return NotFound();
                }
    
                return Ok(brand);
            }
    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> CreateBrandAsync([FromBody] BrandModel brand)
            {
                if (brand == null)
                {
                    return BadRequest();
                }
    
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
    
                var createdBrand = await _brandService.CreateBrandAsync(brand);
                if (createdBrand == null)
                {
                    return BadRequest();
                }
    
                return CreatedAtAction("GetBrandByIdAsync", new { id = createdBrand.Id }, createdBrand);
            }
    
            [HttpPut]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> UpdateBrandAsync([FromBody] BrandModel brand)
            {
                if (brand == null)
                {
                    return BadRequest();
                }
    
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
    
                var updatedBrand = await _brandService.UpdateBrandAsync(brand);
                if (updatedBrand == null)
                {
                    return BadRequest();
                }
    
                return Ok(updatedBrand);
            }
    
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> DeleteBrandAsync(Guid id)
            {
                if (id == Guid.Empty)
                {
                    return BadRequest();
                }
    
                var deleted = await _brandService.DeleteBrandAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
    
                return Ok();
            }
        }
    }
    
    ```
    
- Data: Ce dossier contient le fichier Appdbcontext. Il permet de regrouper tous nos dbset dans un fichier. Il sert également de point déentrée entre la base de donnée et notre backend.
    
    ```csharp
    using Microsoft.EntityFrameworkCore;
    using VehicleManagement.Entities;
    
    namespace VehicleManagement.Data
    {
        public class AppDbContext : DbContext
        {
            public DbSet<Entities.BrandEntity> Brands { get; set; }
            public DbSet<Entities.CarEntity> Cars { get; set; }
    
            public DbSet<Entities.MaintainanceEntity> Maintainances { get; set; }
    
            public DbSet<Entities.VehicleEntity> Vehicles { get; set; }
    
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=app.db");
            }
    
            override protected void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<VehicleEntity>()
                    .HasMany(v => v.Maintainances)
                    .WithOne()
                    .HasForeignKey(m => m.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    
                    .IsRequired();
    
                modelBuilder.Entity<BrandEntity>()
                    .HasData(Shared.SeedData.BrandData.GetBrands().ToArray());
            }
        }
    }
    
    ```
    

<img width="402" alt="vehiclestruc" src="https://github.com/Jael-dev/VehicleManagement/assets/77239264/4303224b-9b6d-491b-b675-f9b03943fa23">


### 4 - VehicleManagementUnitTest

Les tests sont utilisées pour vérifier la conformité aux différentes regles métier. 
<img width="403" alt="teststruct" src="https://github.com/Jael-dev/VehicleManagement/assets/77239264/1e01ca87-ee9b-40a4-a509-29a0aa9f30a1">
```csharp
using Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagementUnitTest
{
    [TestFixture]
    internal class VehicleTest
    {
        VehicleModel vehicle;

        [SetUp]
        public void Setup()
        {
            vehicle = new VehicleModel
            {
                Mileage = 10000,
                Model = new CarModel { MaintenanceFrequency = 5000 },
                Maintainances = new List<MaintainanceModel>(),
                Year = 2020,
                BrandId = Guid.NewGuid(),
                ModelId = Guid.NewGuid(),
                LicensePlate = "1234567",
            };
        }

        [Test]
        public void Asser_LatenessCalculation_IsCorrect()
        {
            // Arrange
            vehicle.Maintainances.Add(new MaintainanceModel { Mileage = 5000, Date = DateTime.Now });
            vehicle.Maintainances.Add(new MaintainanceModel { Mileage = 9000, Date = DateTime.Now });

            // Assert
            Assert.That(vehicle.Lateness,Is.EqualTo(4000));
        }

        [Test]
        public void Asser_LatenessCalculation_IsCorrect_WhenMaintainancesIsNull()
        {
            // Arrange
            vehicle.Maintainances = null;

            // Assert
            Assert.That(vehicle.Lateness,Is.EqualTo(5000));
        }

        [Test]
        public void Asser_LatenessCalculation_IsCorrect_WhenMaintainancesIsEmpty()
        {
            

            // Assert
            Assert.That(vehicle.Lateness,Is.EqualTo(5000));
        }

        [Test]
        public void Assert_LicensePlateLength_IsValid()
        {
            

            // Act
            var context = new ValidationContext(vehicle, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(vehicle, context, results, true);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void Assert_LicensePlateLength_IsInvalid()
        {
            // Arrange
            vehicle.LicensePlate = "123";

            // Act
            var context = new ValidationContext(vehicle, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(vehicle, context, results, true);

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(results[0].ErrorMessage, Is.EqualTo("License plate must be at least 7 characters long"));
            Assert.That(results.Count, Is.EqualTo(1));
        }

        [Test]
        public void Assert_YearRange_IsValid()
        {
            // Arrange
            vehicle.Year = 2020;

            // Act
            var context = new ValidationContext(vehicle, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(vehicle, context, results, true);

            // Assert
            Assert.That(isValid, Is.True);
            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void Assert_YearRange_IsInvalid()
        {
            // Arrange
            vehicle.Year = 1800;

            // Act
            var context = new ValidationContext(vehicle, null, null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(vehicle, context, results, true);

            // Assert
            Assert.That(isValid, Is.False);
            Assert.That(results[0].ErrorMessage, Is.EqualTo("The year must be between 1900 and 2100"));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].MemberNames, Contains.Item("Year"));
        }
    }
}

```

---

## Installation

Cette application est développée en utilisant .NET, alors assurez-vous d'avoir .NET Core 3.1+ installé sur votre machine. Pour installer l'application :

1. Clonez le dépôt
2. Naviguez jusqu'au répertoire du projet
3. Exécutez `dotnet build` pour construire l'application
4. Exécutez `dotnet run` pour démarrer l'appplication
5. Lancer le VehicleManagement.
6. Ensuite lancer FleetManagement.

---

## Contribution

Les contributions sont les bienvenues ! N'hésitez pas à soumettre une pull request.

---

## Licence

Ce projet est sous licence MIT.

Nous espérons que vous trouverez cette application utile. Bonne conduite et gestion de voiture !
