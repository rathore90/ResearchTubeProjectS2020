using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static ResearchTube.Areas.Identity.Pages.Account.Manage.ChangePasswordModel;

namespace ResearchTube.Test
{
    public class TestChangePassword
    {

        [Fact]
        public void TestPasswordLength()
        {
            var inputModel = new InputModel
            {
                NewPassword = "123"
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, true);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The New password must be at least 6 and at max 100 characters long.");
        }

        [Fact]
        public void TestConfirmPassword()
        {
            var inputModel = new InputModel
            {
                NewPassword = "123Qw.",
                ConfirmPassword = "123QQw"
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(inputModel, new ValidationContext(inputModel), validationResults, true);

            Assert.Contains(validationResults, ValidationResult => ValidationResult.ErrorMessage == "The new password and confirmation password do not match.");
        }
    }
}
