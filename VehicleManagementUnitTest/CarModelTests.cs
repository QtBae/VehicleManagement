using Bogus.DataSets;
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
    public class CarModelTests
    {

        [Test]
        public void ModelName_Validation_Success()
        {
            // Arrange
            var carModel = new CarModel
            {
                Id = Guid.NewGuid(),
                ModelName = "Test Model",
                BrandId = Guid.NewGuid(),
                MaintenanceFrequency = 3
            };

            // Act
            var validationContext = new ValidationContext(carModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(carModel, validationContext, results, true);

            // Assert
            Assert.IsEmpty(results);
        }

        [Test]
        public void ModelName_Validation_Failure_MinLength()
        {
            // Arrange
            var carModel = new CarModel
            {
                Id = Guid.NewGuid(),
                ModelName = "", // Empty string, violates MinLength
                BrandId = Guid.NewGuid(),
                MaintenanceFrequency = 3
            };

            // Act
            var validationContext = new ValidationContext(carModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(carModel, validationContext, results, true);


            // Assert
            Assert.IsNotEmpty(results);
            Assert.AreEqual("The model name should be atleast 1 charater", results[0].ErrorMessage);
        }

        [Test]
        public void BrandId_Validation_Success()
        {
            // Arrange
            var carModel = new CarModel
            {
                Id = Guid.NewGuid(),
                ModelName = "Test Model",
                BrandId = Guid.NewGuid(), // A valid GUID
                MaintenanceFrequency = 3
            };

            // Act
            var validationContext = new ValidationContext(carModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(carModel, validationContext, results, true);


            // Assert
            Assert.IsEmpty(results);
        }

        [Test]
        public void BrandId_Validation_Failure_EmptyGuid()
        {
            // Arrange
            var carModel = new CarModel
            {
                Id = Guid.NewGuid(),
                ModelName = "Test Model",
                BrandId = Guid.Empty, // Empty GUID, violates NotEmptyGuid
                MaintenanceFrequency = 3
            };

            // Act
            var validationContext = new ValidationContext(carModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(carModel, validationContext, results, true);


            // Assert
            Assert.IsNotEmpty(results);
            Assert.AreEqual("The brand is required", results[0].ErrorMessage);
        }
    }
}
