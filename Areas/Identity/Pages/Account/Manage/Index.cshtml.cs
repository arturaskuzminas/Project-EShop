using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyShop.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
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
            [Display(Name = "Vardas")]
            [Required(ErrorMessage = "Laukas 'Vardas' yra privalomas")]
            public string FirstName { get; set; }

            [Display(Name = "Pavardė")]
            [Required(ErrorMessage = "Laukas 'Pavardė' yra privalomas")]
            public string LastName { get; set; }

            [Display(Name = "Adresas")]
            [Required(ErrorMessage = "Laukas 'Adresas' yra privalomas")]
            public string Address { get; set; }

            [Display(Name = "Miestas")]
            [Required(ErrorMessage = "Laukas 'Miestas' yra privalomas")]
            public string CityID { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            user = await _userManager.GetUserAsync(User);
            var userName = user.UserName;
            var phoneNumber = user.PhoneNumber;
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var address = user.Address;
            var cityID = user.CityID;

            Input = new InputModel
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                CityID = cityID,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nepavyko užkrauti naudotojo su ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nepavyko užkrauti naudotojo su ID '{_userManager.GetUserId(User)}'.");
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
                    StatusMessage = "Nenumatyta klaida bandant nustatyti telefono numerį.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Jūsų anketa buvo atnaujinta sėkmingai !";
            return RedirectToPage();
        }
    }
}
