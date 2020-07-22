using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static ResearchTube.Areas.Identity.Pages.Account.ResetPasswordModel;

namespace ResearchTube.Test
{
    public class TestResetPassword
    {
        [Fact]
        public void TestPasswordLength()
        {
            var inputModel = new InputModel
            {
                Password = "123"
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, true);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The Password must be at least 6 and at max 100 characters long.");
        }

        [Fact]
        public void TestConfirmPassword()
        {
            var inputModel = new InputModel
            {
                Password = "123Qw.",
                ConfirmPassword = "123QQw"
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, true);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The password and confirmation password do not match.");
        }
    }
}
