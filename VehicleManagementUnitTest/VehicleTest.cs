﻿using Shared.ApiModels;
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

