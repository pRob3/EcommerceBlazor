using EcommerceBlazor.Data;
using Microsoft.AspNetCore.Identity;

namespace EcommerceBlazor.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return await userManager.IsInRoleAsync(user, role);
        }
    }
}
