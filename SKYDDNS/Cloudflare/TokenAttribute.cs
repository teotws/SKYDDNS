using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace SKYDDNS.Cloudflare
{
    public class TokenAttribute : ApiActionAttribute
    {
        public override Task OnRequestAsync(ApiRequestContext context)
        {
            var config = context.HttpContext.ServiceProvider.GetRequiredService<IConfiguration>();
            var token = config.GetValue<string>("CF_Token");
            context.HttpContext.RequestMessage.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");
            return Task.CompletedTask;
        }
    }
}
