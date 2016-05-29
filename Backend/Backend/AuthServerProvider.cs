using Backend.PasswordHasher;
using Backend.Service;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class AuthServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IService _service;
        private readonly IPasswordHasher _passwordHasher;

        public AuthServerProvider(IService service, IPasswordHasher passwordHasher)
        {
            _service = service;
            _passwordHasher = passwordHasher;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext credentialsContext)
        {
            var user =  _service.GetUserEntityByEmail(credentialsContext.UserName);
            if (user != null)
            {
                if (!_passwordHasher.VerifyHash(credentialsContext.Password, user.PasswordHash, user.PasswordSalt))
                {
                    credentialsContext.Rejected();
                    return;
                }


                var identity = new ClaimsIdentity("Bearer");
                identity.AddClaim(new Claim(identity.NameClaimType, credentialsContext.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

                credentialsContext.Validated(identity);
                return;

            }

            credentialsContext.Rejected();
        }
    }
}
