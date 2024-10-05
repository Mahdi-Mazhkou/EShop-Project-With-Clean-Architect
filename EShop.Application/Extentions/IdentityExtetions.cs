using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Extentions
{
    public static class IdentityExtetions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            string userName = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == "UserName")?.Value;
            return userName;
        }
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return default;
            }
            return int.Parse(userId);
        }
        
    }
}
