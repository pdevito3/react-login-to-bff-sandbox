namespace CarbonKitchenBff
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Duende.Bff;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;

    public class ApiLoginService : ILoginService
    {
        private readonly BffOptions _options;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        public ApiLoginService(BffOptions options)
        {
            _options = options;
        }
        
        public async Task ProcessRequestAsync(HttpContext context)
        {
            var returnUrl = context.Request.Query[Constants.RequestParameters.ReturnUrl].FirstOrDefault();

            var props = new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? "/"
            };

            await context.ChallengeAsync(props);
        }
    }
}