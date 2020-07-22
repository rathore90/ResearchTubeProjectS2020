using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static ResearchTube.Areas.Identity.Pages.Account.RegisterModel;

namespace ResearchTube.Test
{
    public class TestRegisterForm
    {
        [Fact]
        public void TestConfirmPassword()
        {
            var inputModel = new InputModel
            {
                Password = "123Qw.",
                ConfirmPassword = "123Qww."
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, true);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The password and confirmation password do not match.");
        }


        [Fact]
        public void TestInterest()
        {
            var inputModel = new InputModel
            {
                Interest = null
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, false);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The Interest field is required.");
        }

        [Fact]
        public void TestFirstName()
        {
            var inputModel = new InputModel
            {
                FirstName = null
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, false);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The First Name field is required.");
        }


        [Fact]
        public void TestLastName()
        {
            var inputModel = new InputModel
            {
                LastName = null
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, false);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The Last Name field is required.");
        }


        [Fact]
        public void TestEmail()
        {
            var inputModel = new InputModel
            {
                Email = null
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, false);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The Email field is required.");
        }


        [Fact]
        public void TestPassword()
        {
            var inputModel = new InputModel
            {
                Password = null
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, false);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The Password field is required.");
        }


        [Fact]
        public void TestPosition()
        {
            var inputModel = new InputModel
            {
                Position = null
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, false);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The Position field is required.");
        }


        [Fact]
        public void TestNullVar()
        {
            var inputModel = new InputModel
            {
                UploadImage = null
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, true);

            Assert.DoesNotContain(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The Upload Image field is required.");
        }

    }
}
