using Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagementUnitTest
{
    internal class MaintainanceModelTests
    {
        [Test]
        public void Works_Validation_Success()
        {
            // Arrange
            var maintenanceModel = new MaintainanceModel
            {
                Id = Guid.NewGuid(),
                Works = "Test Works",
                Mileage = 5000,
                Date = DateTime.Now,
                VehicleId = Guid.NewGuid()
            };

            // Act
            var validationContext = new ValidationContext(maintenanceModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(maintenanceModel, validationContext, results, true);

            // Assert
            Assert.IsEmpty(results);
        }

        [Test]
        public void Works_Validation_Failure_Required()
        {
            // Arrange
            var maintenanceModel = new MaintainanceModel
            {
                Id = Guid.NewGuid(),
                Works = null, // Null value, violates Required
                Mileage = 5000,
                Date = DateTime.Now,
                VehicleId = Guid.NewGuid()
            };

            // Act
            var validationContext = new ValidationContext(maintenanceModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(maintenanceModel, validationContext, results, true);

            // Assert
            Assert.IsNotEmpty(results);
            Assert.AreEqual("The works are required", results[0].ErrorMessage);
        }

        [Test]
        public void Mileage_Validation_Success()
        {
            // Arrange
            var maintenanceModel = new MaintainanceModel
            {
                Id = Guid.NewGuid(),
                Works = "Test Works",
                Mileage = 5000,
                Date = DateTime.Now,
                VehicleId = Guid.NewGuid()
            };

            // Act
            var validationContext = new ValidationContext(maintenanceModel);
            var results = new List<ValidationResult>();

            // Assert
            var isValid = Validator.TryValidateObject(maintenanceModel, validationContext, results, true);
        }

        [Test]
        public void Mileage_Validation_Failure_Required()
        {
            // Arrange
            var maintenanceModel = new MaintainanceModel
            {
                Id = Guid.NewGuid(),
                Works = "Test Works",
                Mileage = 0, // Zero mileage, violates Range and Required
                Date = DateTime.Now,
                VehicleId = Guid.NewGuid()
            };

            // Act
            var validationContext = new ValidationContext(maintenanceModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(maintenanceModel, validationContext, results, true);

            // Assert
            Assert.IsNotEmpty(results);
            Assert.AreEqual("The mileage is required", results[0].ErrorMessage);
        }
    }

        // Add tests for other properties (Date, VehicleId) similarly
    }
