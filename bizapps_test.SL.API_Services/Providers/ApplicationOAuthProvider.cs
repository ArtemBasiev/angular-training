using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Services;
using Microsoft.Owin.Security.OAuth;


namespace bizapps_test.SL.API_Services.Providers
{
    public class ApplicationOAuthProvider: OAuthAuthorizationServerProvider
    {

        private IUserService _userService;

        public ApplicationOAuthProvider(IUserService userService)
        {
            _userService = userService;
        }
    

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var serviceAnswer = _userService.GetUserByName(context.UserName);

            if (serviceAnswer.Status == AnswerStatus.Failed)
            {
                context.SetError("inner_service_error");
            }
            else
            {
                var userDTO = serviceAnswer.ReceivedEntity;

                if (userDTO == null)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                }
                else
                {
                    if (context.UserName == userDTO.UserName && context.Password == userDTO.UserPassword)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                        identity.AddClaim(new Claim("username", userDTO.UserName));
                        identity.AddClaim(new Claim(ClaimTypes.Name, userDTO.UserName));
                        context.Validated(identity);
                        
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Provided username and password is incorrect");
                    }
                }
            }
        }

    }

}