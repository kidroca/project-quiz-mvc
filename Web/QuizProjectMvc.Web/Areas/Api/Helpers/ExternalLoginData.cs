namespace QuizProjectMvc.Web.Areas.Api.Helpers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNet.Identity;

    public class ExternalLoginData
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }

        public string UserName { get; set; }

        public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || string.IsNullOrEmpty(providerKeyClaim.Issuer)
                || string.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            return new ExternalLoginData
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                UserName = identity.FindFirstValue(ClaimTypes.Name)
            };
        }

        public IList<Claim> GetClaims()
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, this.ProviderKey, null, this.LoginProvider));

            if (this.UserName != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, this.UserName, null, this.LoginProvider));
            }

            return claims;
        }
    }
}
