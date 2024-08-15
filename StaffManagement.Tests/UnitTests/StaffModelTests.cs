using NUnit.Framework;
using StaffManagementAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace StaffManagementAPI.Tests.UnitTests
{
    public class StaffModelTests
    {
        [Test]
        public void StaffModel_ValidData_ShouldPassValidation()
        {
            // Arrange
            var staff = new Staff
            {
                StaffID = "12345678",
                FullName = "John Doe",
                BirthDay = new DateTime(1990, 1, 1),
                Gender = 1
            };

            // Act & Assert
            NUnit.Framework.Assert.DoesNotThrow(() => ValidateModel(staff));
        }

        [Test]
        public void StaffModel_InvalidGender_ShouldFailValidation()
        {
            // Arrange
            var staff = new Staff
            {
                StaffID = "12345678",
                FullName = "John Doe",
                BirthDay = new DateTime(1990, 1, 1),
                Gender = 3 // Invalid gender
            };

            // Act & Assert
            var validationResults = ValidateModel(staff);
            NUnit.Framework.Assert.IsTrue(validationResults.Count > 0);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
