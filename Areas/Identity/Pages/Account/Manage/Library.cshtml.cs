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
    public class LibraryModel : PageModel
    {
        private readonly UserManager<ResearchTubeUser> _userManager;
        private readonly SignInManager<ResearchTubeUser> _signInManager;


        public LibraryModel(
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
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "VideoId", Prompt = "VideoId")]
            public string Videoid { get; set; }
        }
        private async Task LoadAsync(ResearchTubeUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            Username = Username;
            Input = new InputModel
            {
               Videoid = user.VideoId

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
            if(Input.Videoid != user.VideoId)
            {
                user.VideoId = Input.Videoid;
            }
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
