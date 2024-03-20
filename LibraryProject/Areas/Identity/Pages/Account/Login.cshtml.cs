
using System.ComponentModel.DataAnnotations;
using LibraryProject.DataAccess.Models;
using LibraryProject.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace LibraryProject.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(SignInManager<User> signInManager, IJwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string EmailOrUsername { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var isEmail = new EmailAddressAttribute().IsValid(Input.EmailOrUsername);
                var user = isEmail ? await _signInManager.UserManager.FindByEmailAsync(Input.EmailOrUsername)
                                   : await _signInManager.UserManager.FindByNameAsync(Input.EmailOrUsername);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Input.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var token = _jwtTokenService.GenerateToken(user);

                        // Store the token securely as a cookie
                        _httpContextAccessor.HttpContext.Response.Cookies.Append("jwtToken", token, new CookieOptions
                        {
                            Expires = DateTime.UtcNow.AddDays(7), 
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict
                        });

                        
                        return LocalRedirect(returnUrl);
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            return Page();
        }
    }
}
