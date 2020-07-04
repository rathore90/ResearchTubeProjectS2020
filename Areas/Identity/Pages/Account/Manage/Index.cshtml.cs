using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResearchTube.Areas.Identity.Data;

namespace ResearchTube.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ResearchTubeUser> _userManager;
        private readonly SignInManager<ResearchTubeUser> _signInManager;

        public IndexModel(
            UserManager<ResearchTubeUser> userManager,
            SignInManager<ResearchTubeUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name", Prompt = "First Name")]
            public string FirstName { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name", Prompt = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Position", Prompt = "Position")]
            public string Position { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Interest", Prompt = "Interest")]
            public string Interest { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Country", Prompt = "Country")]
            public string Country{ get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Province", Prompt = "Province")]
            public string Province { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Address", Prompt = "Address")]
            public string Address{ get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Postcode", Prompt = "Postcode")]
            public string Postcode { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "University", Prompt = "University")]
            public string University { get; set; }

        }

        private async Task LoadAsync(ResearchTubeUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Interest = user.Interest,
                Position = user.Position,
                Country = user.Country,
                Province = user.Province,
                Address = user.Address,
                Postcode = user.Postcode,
                University = user.University
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (Input.Interest != user.Interest)
            {
                user.Interest = Input.Interest;
            }

            if (Input.Position != user.Position)
            {
                user.Position = Input.Position;
            }

            if (Input.Country != user.Country)
            {
                user.Country = Input.Country;
            }

            if (Input.Province != user.Province)
            {
                user.Province = Input.Province;
            }

            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
            }

            if (Input.Postcode != user.Postcode)
            {
                user.Postcode = Input.Postcode;
            }

            if (Input.University != user.University)
            {
                user.University = Input.University;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
