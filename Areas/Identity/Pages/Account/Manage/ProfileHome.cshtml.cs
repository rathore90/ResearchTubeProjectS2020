using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResearchTube.Areas.Identity.Data;
using ResearchTube.Data;

namespace ResearchTube.Areas.Identity.Pages.Account.Manage
{
    public class ProfileHomeModel : PageModel
    {
        private readonly UserManager<ResearchTubeUser> _userManager;
        private readonly SignInManager<ResearchTubeUser> _signInManager;
        private readonly ResearchTubeDbContext _db_context;

        public ProfileHomeModel(
            UserManager<ResearchTubeUser> userManager,
            SignInManager<ResearchTubeUser> signInManager,
            ResearchTubeDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db_context = dbContext;
        }

        public string Username { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            public string PhoneNumber { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Upload Image", Prompt = "Profile Image")]
            public string? UploadImage { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Interest { get; set; }
            public string Position { get; set; }
        }

        private async Task LoadAsync(ResearchTubeUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);


            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                UploadImage = user.UploadImage
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

        public async Task<IActionResult> OnPostAsync(IFormFile fileobj, ResearchTubeUser rtu, string? returnUrl = null)
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
            if (fileobj != null)
            {
                String imgText = Path.GetExtension(fileobj.FileName).ToLower();
                if (imgText == ".jpg" || imgText == ".jpeg" || imgText == ".png")
                {
                    //Store in database
                    user.UploadImage = "~/Images/" + fileobj.FileName;
                    await _db_context.SaveChangesAsync();
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


            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}

